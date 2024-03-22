using Quartz;

namespace QuartzExample;

public class KeepSessionJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        // Se code chi tiet luu giu session o day
        Console.WriteLine("KeepSessionJob is running, in real will code business login in here. Time: " + DateTime.Now);
        return Task.CompletedTask;
    }
}
