namespace WorkerWeather
{
    public class Worker : BackgroundService
    {
        private IServiceProvider _serviceProvider;
        public Worker(ILogger<Worker> logger, IConfiguration configs, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                DataScheduler.Start(_serviceProvider);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
