using System;
using System.Text.RegularExpressions;
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
        endpointConfiguration
            .AdvancedConfiguration
            .Conventions()
            .DefiningCommandsAs((Func<Type, bool>)(t => Regex.IsMatch(t.Name, "Command(V\\d+)?$")))
            .DefiningEventsAs((Func<Type, bool>)(t => Regex.IsMatch(t.Name, "Event(V\\d+)?$")));
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
