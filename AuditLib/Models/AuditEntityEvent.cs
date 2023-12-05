using Audit.Core;
using Audit.EntityFramework;
using Newtonsoft.Json;

namespace AuditLib
{
    public sealed class AuditEntityEvent : AuditEvent
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 10)]
        public List<EntityFrameworkEventExtend>? EntityEventInfo { get; set; }

    }
    public sealed class EntityFrameworkEventExtend : EntityFrameworkEvent
    {
        public EntityFrameworkEventExtend(EntityFrameworkEvent data)
        {
            if (data != null)
            {
                this.AmbientTransactionId = data.AmbientTransactionId;
                this.ConnectionId = data.ConnectionId;
                this.CustomFields = data.CustomFields;
                this.Database = data.Database;
                this.Entries = data.Entries;
                this.ErrorMessage = data.ErrorMessage;
                this.Result = data.Result;
                this.Success = data.Success;
                this.TransactionId = data.TransactionId;
            }
        }
        public long ExecutionTime { get; set; }
        public string? ServerName { get; set; }
    }
}
