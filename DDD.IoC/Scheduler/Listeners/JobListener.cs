using DDD.IoC.Core;
using Quartz;

namespace DDD.IoC.Scheduler.Listeners
{
    public class JobListener : IJobListener
    {
        public string Name => "JobListener";

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
           //
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            Logger.Custom($"Job Executed: {context.JobDetail.Key.Name}", "jobs");
        }
    }
}
