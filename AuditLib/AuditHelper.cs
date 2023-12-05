using Audit.Core;
using DTO;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace AuditLib
{
    public sealed class AuditHelper : IAuditHelper
    {
        private AuditLogConfig _auditLogConfig;
        public AuditHelper(IOptionsMonitor<AuditLogConfig> auditLogConfig, AuditCustomDataProvider auditCustomDataProvider)
        {
            _auditLogConfig = auditLogConfig.CurrentValue;
            if (!_auditLogConfig.Enable)
            {
                Configuration.AuditDisabled = true;
            }
            Configuration.Setup().UseCustomProvider(auditCustomDataProvider).WithCreationPolicy(EventCreationPolicy.Manual);
            auditLogConfig.OnChange(value =>
            {
                if (!value.Enable)
                {
                    Configuration.AuditDisabled = true;
                }
                _auditLogConfig = value;
            });

        }
        public Dictionary<string, string>? GetVariables(IEnumerable<KeyValuePair<string, StringValues>> formCollection)
        {
            try
            {
                if (formCollection == null)
                {
                    return null;
                }
                return formCollection.ToDictionary(x => x.Key, v => v.Value.ToString());
            }
            catch (InvalidDataException)
            {
                return null;
            }
        }

        public Dictionary<string, string>? GetHeaders(IEnumerable<KeyValuePair<string, StringValues>> col)
        {
            if (col == null)
            {
                return null;
            }
            if (_auditLogConfig.AllowHeader != null)
            {
                Dictionary<string, string> dict = new();
                foreach (var k in col)
                {
                    if (_auditLogConfig.AllowHeader.Find(x => x.Equals(k.Key, StringComparison.OrdinalIgnoreCase)) != null)
                        dict.Add(k.Key, k.Value.ToString());
                }
                return dict.Count > 0 ? dict : null;
            }
            return null;
        }

        public bool IsAudit(string method)
        {
            return _auditLogConfig.Enable && (_auditLogConfig.ServiceUnAudit == null || _auditLogConfig.ServiceUnAudit.Count == 0 || _auditLogConfig.ServiceUnAudit.Find(c => c.StartsWith(method)) == null);
        }

        public bool IsAuditRequest(string method)
        {
            return _auditLogConfig.Enable && (_auditLogConfig.ServiceUnAuditRequest == null || _auditLogConfig.ServiceUnAuditRequest.Count == 0 || _auditLogConfig.ServiceUnAuditRequest.Find(c => c.StartsWith(method)) == null);
        }

        public bool IsAuditResponse(string method)
        {
            return _auditLogConfig.Enable && (_auditLogConfig.ServiceUnAuditResponse == null || _auditLogConfig.ServiceUnAuditResponse.Count == 0 || _auditLogConfig.ServiceUnAuditResponse.Find(c => c.StartsWith(method)) == null);
        }
    }

    public interface IAuditHelper
    {
        bool IsAudit(string method);
        bool IsAuditRequest(string method);
        bool IsAuditResponse(string method);
        Dictionary<string, string>? GetVariables(IEnumerable<KeyValuePair<string, StringValues>> formCollection);
        Dictionary<string, string>? GetHeaders(IEnumerable<KeyValuePair<string, StringValues>> headers);

    }
}
