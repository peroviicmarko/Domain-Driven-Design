using DDD.IoC.Core;
using Quartz;

namespace DDD.IoC.Scheduler.Listeners
{
    public class SchedulerListener : ISchedulerListener
    {
        public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            Logger.Error($"SchedulerError: {msg}");
            Logger.Error(cause.Message);
        }

        public async Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task TriggersPaused(string? triggerGroup, CancellationToken cancellationToken = default)
        {
            //
        }

        public async Task TriggersResumed(string? triggerGroup, CancellationToken cancellationToken = default)
        {
            //
        }
    }
}
