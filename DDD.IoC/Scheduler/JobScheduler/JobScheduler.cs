using DDD.IoC.Core;
using Quartz;

namespace DDD.IoC.Scheduler.JobScheduler
{
    public class JobScheduler
    {
        private static IScheduler _scheduler;

        public JobScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task ScheduleSotrageManager(string time)
        {
            try
            {
                string token = Guid.NewGuid().ToString();

                IJobDetail job = JobBuilder.CreateStorageManagerJob(token);
                ITrigger trigger = TriggerBuilder.CreateTrigger(token, time, job);

                await _scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}
