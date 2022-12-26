using DDD.IoC.Core;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace DDD.IoC.Scheduler.Factory
{
    public class JobFactory : SimpleJobFactory
    {

        private IServiceProvider _serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
            }
            catch (Exception ex)
            {
                Logger.Error($"Problem while instatiating Job {bundle.JobDetail.Key}. {ex.GetBaseException().Message}");
                throw new SchedulerException(ex);
            }
        }

    }
}
