using Audit.Core;
using Serilog;

namespace AuditLib
{
    public static class AuditScopeExtend
    {
        public static void CommentAudit(this AuditScope? scope, string message, params object[] args)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    if (args == null)
                        scope?.Comment("{0:HH:mm:ss.fff}: {1}", DateTime.Now, message);
                    else
                    {
                        var commentMessage = $"{DateTime.Now:HH:mm:ss.fff}: {message}";
                        scope?.Comment(commentMessage, args);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Audit Comment Exception");
            }
        }
    }

}
