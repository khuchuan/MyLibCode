namespace AuditLib
{
    public sealed class AuditLogConfig
    {
        public const string Name = "AuditLog";
        public bool Enable { get; set; }
        public string Collection { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string[] SensitiveDataJson { get; set; } = Array.Empty<string>();
        public List<string> AllowHeader { get; set; } = new List<string>();
        public bool ExcludeResponseBody { get; set; }
        public List<string> ServiceUnAudit { get; set; } = new List<string>();
        public List<string> ServiceUnAuditRequest { get; set; } = new List<string>();
        public List<string> ServiceUnAuditResponse { get; set; } = new List<string>();

        public int CheckConnectionTimeoutInSeconds { get; set; } = 5;
        public int CheckConnectionIntervalInSeconds { get; set; } = 60;
        public int UpdateIntervalInSeconds { get; set; } = 3;
    }
}
