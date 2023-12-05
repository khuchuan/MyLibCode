using Audit.Core;
using Audit.Core.Providers;
using Helper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Serilog;

namespace AuditLib
{
    public sealed class AuditCustomDataProvider : AuditDataProvider
    {
        private readonly FileDataProvider _fileDataProvider = new();
        private bool _mongoAlive;
        private readonly Timer _checkConnectionTimer;
        private readonly Timer _enqueueTimer;
        private readonly IAuditBackgroundTaskQueue _taskQueue;
        private readonly List<AuditEvent> _auditQueue = new();
        private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
        private IMongoCollection<BsonDocument>? _collection;
        private IMongoDatabase? _database;
        private AuditLogConfig _auditLogConfig;
        private JsonSerializer? _serializer;

        public AuditCustomDataProvider(IOptionsMonitor<AuditLogConfig> auditLogConfig, IAuditBackgroundTaskQueue taskQueue)
        {
            _taskQueue = taskQueue;
            _auditLogConfig = auditLogConfig.CurrentValue;

            if (_auditLogConfig.Enable)
            {
                var settings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatString = "dd/MM/yyyy HH:mm:ss.fff",
                    ContractResolver = new SensitiveContractResolver(_auditLogConfig.SensitiveDataJson),
                };
                _serializer = JsonSerializer.Create(settings);
                var client = new MongoClient(auditLogConfig.CurrentValue.ConnectionString);
                _database = client.GetDatabase(auditLogConfig.CurrentValue.Database);
                _collection = _database.GetCollection<BsonDocument>(auditLogConfig.CurrentValue.Collection);
                CheckConnection();
                _checkConnectionTimer = new Timer(CheckConnection, null, TimeSpan.FromSeconds(_auditLogConfig.CheckConnectionIntervalInSeconds), TimeSpan.FromSeconds(_auditLogConfig.CheckConnectionIntervalInSeconds));
                _enqueueTimer = new Timer(Enqueue, null, TimeSpan.FromSeconds(_auditLogConfig.UpdateIntervalInSeconds), TimeSpan.FromSeconds(_auditLogConfig.UpdateIntervalInSeconds));
            }
            else
            {
                _checkConnectionTimer = new Timer(CheckConnection, null, Timeout.Infinite, Timeout.Infinite);
                _enqueueTimer = new Timer(Enqueue, null, Timeout.Infinite, Timeout.Infinite);
            }
            auditLogConfig.OnChange(value =>
            {
                if (value.Enable)
                {
                    var settings = new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatString = "dd/MM/yyyy HH:mm:ss.fff",
                        ContractResolver = new SensitiveContractResolver(_auditLogConfig.SensitiveDataJson),
                        //Converters = { new SensitiveDictionaryConverter(_auditLogConfig.SensitiveDataJson) }
                    };
                    _serializer = JsonSerializer.Create(settings);
                    if (_auditLogConfig.Enable != value.Enable || !_auditLogConfig.ConnectionString.Equals(value.ConnectionString) || !_auditLogConfig.ConnectionString.Equals(value.ConnectionString) || !_auditLogConfig.Collection.Equals(value.Collection))
                    {
                        var client = new MongoClient(value.ConnectionString);
                        _database = client.GetDatabase(value.Database);
                        _collection = _database.GetCollection<BsonDocument>(value.Collection);
                        CheckConnection();
                        _checkConnectionTimer.Change(TimeSpan.FromSeconds(_auditLogConfig.CheckConnectionIntervalInSeconds), TimeSpan.FromSeconds(_auditLogConfig.CheckConnectionIntervalInSeconds));
                        _enqueueTimer.Change(TimeSpan.FromSeconds(_auditLogConfig.UpdateIntervalInSeconds), TimeSpan.FromSeconds(_auditLogConfig.UpdateIntervalInSeconds));
                    }
                }
                else
                {
                    _checkConnectionTimer?.Change(Timeout.Infinite, Timeout.Infinite);
                    _enqueueTimer?.Change(Timeout.Infinite, Timeout.Infinite);
                    _database = null;
                    _collection = null;
                }
                _auditLogConfig = value;
            });


            _fileDataProvider.DirectoryPathBuilder = (e) => $"{AppContext.BaseDirectory}/AuditLog/{DateTime.Now:ddMMyyyy}";
        }
        private async void Enqueue(object? state)
        {
            await _semaphoreSlim.WaitAsync();

            if (_auditQueue.Count > 0)
            {
                var temp = _auditQueue.ToArray();
                _auditQueue.Clear();
                await _taskQueue.QueueBackgroundWorkItemAsync(async ct => await InsertEventsAsync(temp, ct));
            }
            _semaphoreSlim.Release();
        }
        private void CheckConnection(object? state = null)
        {
            try
            {
                var checkConnectionTask = Task.Run(async () =>
                {
                    try
                    {
                        return await TestConnectionAsync();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "CheckConnection");
                        return false;
                    }
                });
                if (checkConnectionTask.Wait(TimeSpan.FromSeconds(_auditLogConfig.CheckConnectionTimeoutInSeconds)))
                {
                    _mongoAlive = checkConnectionTask.Result;
                }
                else
                    _mongoAlive = false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "CheckConnection");
                _mongoAlive = false;
            }
        }
        public async Task<bool> TestConnectionAsync()
        {
            return _database != null && (await _database.RunCommandAsync<BsonDocument>("{ping:1}"))["ok"].ToInt64() == 1;
        }
        public override async Task<object> InsertEventAsync(AuditEvent auditEvent, CancellationToken cancellationToken)
        {
            try
            {
                if (_mongoAlive)
                {
                    await _semaphoreSlim.WaitAsync();
                    _auditQueue.Add(auditEvent);
                    _semaphoreSlim.Release();
                    return true;
                }
                else
                    return await InsertFileAsync(auditEvent);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "InsertEventAsync");
                return await InsertFileAsync(auditEvent);
            }
        }

        public override object InsertEvent(AuditEvent auditEvent)
        {
            try
            {
                if (_mongoAlive)
                {
                    _semaphoreSlim.Wait();
                    _auditQueue.Add(auditEvent);
                    _semaphoreSlim.Release();
                    return true;
                }
                else
                    return InsertFile(auditEvent);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "InsertEvent");
                return InsertFile(auditEvent);
            }
        }
        private async Task<object> InsertFileAsync(AuditEvent auditEvent)
        {
            try
            {
                return await _fileDataProvider.InsertEventAsync(auditEvent);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "InsertFileAsync");
            }
            return false;
        }
        private object InsertFile(AuditEvent auditEvent)
        {
            try
            {
                return _fileDataProvider.InsertEvent(auditEvent);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "InsertFile");
            }
            return false;
        }
        public async ValueTask InsertEventsAsync(AuditEvent[] auditEvents, CancellationToken token)
        {
            try
            {
                if (_collection != null)
                {
                    var documents = ParseBson(auditEvents);
                    if (documents.Count == 1)
                        await _collection.InsertOneAsync(documents[0]);
                    else
                        await _collection.InsertManyAsync(documents);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "InsertEventsAsync");
                foreach (var item in auditEvents)
                {
                    await InsertFileAsync(item);
                }
            }

        }
        private List<BsonDocument> ParseBson(AuditEvent[] auditEvents)
        {
            var result = new List<BsonDocument>(auditEvents.Length);
            using var ms = new MemoryStream();
            using var writer = new BsonDataWriter(ms)
            {
                DateTimeKindHandling = DateTimeKind.Utc
            };
            for (int i = 0; i < auditEvents.Length; i++)
            {
                ms.SetLength(0);
                _serializer?.Serialize(writer, auditEvents[i]);
                ms.Seek(0, SeekOrigin.Begin);
                using var bsonReader = new BsonBinaryReader(ms);
                var context = BsonDeserializationContext.CreateRoot(bsonReader);
                result.Add(BsonDocumentSerializer.Instance.Deserialize(context));
            }
            return result;
        }
        private BsonDocument ParseBson(AuditEvent auditEvent)
        {
            using var ms = new MemoryStream();
            using var writer = new BsonDataWriter(ms)
            {
                DateTimeKindHandling = DateTimeKind.Utc
            };
            _serializer?.Serialize(writer, auditEvent);
            ms.Seek(0, SeekOrigin.Begin);
            using var bsonReader = new BsonBinaryReader(ms);
            var context = BsonDeserializationContext.CreateRoot(bsonReader);
            return BsonDocumentSerializer.Instance.Deserialize(context);
        }
    }

}
