using Quartz;
using Quartz.Impl;
using Service.Jobs.Weather;

namespace WorkerWeather
{
    public static class DataScheduler
    {
        public static async void Start(IServiceProvider serviceProvider)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetService<WeatherSnifferFactory>();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<WeatherSnifferJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("WeatherTrigger", "WeatherGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(5)
                //.WithIntervalInMinutes(1)
                .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
