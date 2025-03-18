using Repository.DbContext;
using Service.Extensions;
using WorkerWeather;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false);
    })
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        EnvReader.Load(".env");
        var apiKey = Environment.GetEnvironmentVariable("API_KEY");
        if (String.IsNullOrEmpty(apiKey))
            throw new KeyNotFoundException($"The API_KEY does not exist in .env .");

        services.Configure<DatabaseSettings>(
            configuration.GetSection("MongoDb"));

        services.AddBLLServices();

        services.AddHostedService<Worker>();

    })
    .Build();

await host.RunAsync();
