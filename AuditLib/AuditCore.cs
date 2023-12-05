using Audit.Core;

namespace AuditLib
{
    public sealed class AuditCore
    {
        public AuditCore()
        {
        }
        public AuditScope? CurrentScope
        {
            get; set;
        }
    }
}
