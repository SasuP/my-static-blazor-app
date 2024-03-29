using Api;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



var host = new HostBuilder()
/*    .ConfigureFunctionsWorkerDefaults()*/
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<IProductData, ProductData>();
    })
    .Build();

host.Run();
