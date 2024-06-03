using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PollyConsole;

internal sealed class ExecuteHelper
{
    public bool Fail { get; set; } = true;

    public HttpResponseMessage ExecuteUnstable()
    {
        if (Fail)
        {
            Fail = false;
            Console.WriteLine($"{DateTime.UtcNow}: Execute failed");
            throw new InvalidOperationException();
        }

        Fail = true;
        Console.WriteLine($"{DateTime.UtcNow}: Executed");
        return new HttpResponseMessage(HttpStatusCode.OK);
    }
}