using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

[assembly: NServiceBusTriggerFunction("NS.SB.Sender.Function")]

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .UseNServiceBus((configuration, endpointConfiguration) =>
    {
        endpointConfiguration.AdvancedConfiguration.EnableInstallers();
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
