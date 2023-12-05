using Audit.Core;

namespace AuditLib
{
    public sealed class CustomAuditEvent : AuditEvent
    {
        public string? UserName { get; set; }
        public object? RequestBody { get; set; }
        public object? ResponseBody { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public Dictionary<string, string>? FormVariables { get; set; }
        public Dictionary<string, string>? QueryVariables { get; set; }
        public string? IpAddress { get; set; }
        public string? TraceId { get; set; }
        public string? Exception { get; set; }
        public int ResponseStatusCode { get; set; }
        public string? ResponseStatus { get; set; }

    }
}
