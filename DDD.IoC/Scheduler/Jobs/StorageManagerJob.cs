using DDD.IoC.Core;
using Quartz;

namespace DDD.IoC.Scheduler.Jobs
{
    public class StorageManagerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                string[] directories = new string[] { "Info", "warn", "http", "debug", "jobs" };

                foreach (var directory in directories)
                {
                    var directoryPath = Path.Combine(logDirectory, directory);

                    if (Directory.Exists(directoryPath))
                    {
                        Directory.Delete(directoryPath, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.GetBaseException().Message);
            }
        }
    }
}
