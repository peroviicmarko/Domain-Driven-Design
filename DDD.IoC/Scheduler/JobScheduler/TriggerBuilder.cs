using Quartz;

namespace DDD.IoC.Scheduler.JobScheduler
{
    public class TriggerBuilder
    {
        public static ITrigger CreateTrigger(string token, DateTime time, IJobDetail jobDetail)
        {
            ITrigger trigger = Quartz.TriggerBuilder.Create()
                .ForJob(jobDetail)
                .WithIdentity(token, token)
                .StartAt(time)
                .Build();

            return trigger;
        }

        public static ITrigger CreateTrigger(string token, string time, IJobDetail jobDetail)
        {
            ITrigger trigger = Quartz.TriggerBuilder.Create()
                .ForJob(jobDetail)
                .WithIdentity(token, token)
                .WithCronSchedule(time)
                .Build();

            return trigger;
        }
    }
}
