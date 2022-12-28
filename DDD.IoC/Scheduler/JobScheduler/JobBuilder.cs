using DDD.IoC.Scheduler.Jobs;
using Quartz;

namespace DDD.IoC.Scheduler.JobScheduler
{
    public class JobBuilder
    {
        public static IJobDetail CreateStorageManagerJob(string token)
        {
            IJobDetail jobDetail = Quartz.JobBuilder.Create<StorageManagerJob>()
                .WithIdentity(token, token)
                .StoreDurably()
                .RequestRecovery()
                .Build();

            return jobDetail;
        }
    }
}
