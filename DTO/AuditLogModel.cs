using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DTO
{
    public class GetAuditLogsRequest
    {
        public string OrderData { get; set; }
        public string OrderDir { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Collection { get; set; }

    }

    public abstract class BaseAuditLog
    {
        // if you'd like to delegate another property to map onto _id then you can decorate it with the BsonIdAttribute, like this
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class AuditLog : BaseAuditLog
    {
        public string EventType { get; set; }
        public object Environment { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public object Headers { get; set; }
        public string Exception { get; set; }
        public string[] Comments { get; set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public string TraceId { get; set; }
        public object FormVariables { get; set; }
        public object QueryVariables { get; set; }
        public object RequestBody { get; set; }
        public int ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public object ResponseBody { get; set; }
        //public object[] EntityEventInfo { get; set; }
    }

}
