using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollyClient;

public class PollyBusiness
{
    public void Demo1()
    {

        // Define our circuit breaker strategy: break if the action fails at least 4 times in a row.
        var circuitBreakerStrategy = new ResiliencePipelineBuilder().AddCircuitBreaker(new()
        {
            ShouldHandle = new PredicateBuilder().Handle<Exception>(),
            FailureRatio = 1.0,
            SamplingDuration = TimeSpan.FromSeconds(2),
            MinimumThroughput = 4,
            BreakDuration = TimeSpan.FromSeconds(3),
            OnOpened = args =>
            {
                Console.WriteLine("Goi vao ham OnOpend");
                return default;
            },
            OnClosed = args =>
            {
                Console.WriteLine("Goi vao ham OnClosed");
                return default;
            },
            OnHalfOpened = args =>
            {
                Console.WriteLine("Goi vao ham OnHalfOpened");
                return default;
            }
        }).Build();
    }


}
