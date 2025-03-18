using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Service.Jobs.Weather
{
    public class WeatherSnifferFactory : IJobFactory
    {
        protected readonly IServiceScopeFactory serviceScopeFactory;

        public WeatherSnifferFactory(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var job = scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                return job;
            }
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
