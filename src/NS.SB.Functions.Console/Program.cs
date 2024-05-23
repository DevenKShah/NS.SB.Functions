using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NS.SB.Functions.Messages;

var host = new HostBuilder()
    .ConfigureAppConfiguration(builder => builder.AddJsonFile("appsettings.json"))
    .ConfigureServices((context, services) =>
    {
        // Configure NServiceBus
        var endpointConfiguration = new EndpointConfiguration("NS.SB.Functions.Console");

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.ConnectionString(context.Configuration["AzureWebJobsServiceBus"]);

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(SendEmailCommand), "NS.SB.Handler.Function");

        endpointConfiguration.UsePersistence<LearningPersistence>();
        endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
        endpointConfiguration.Conventions()
            .DefiningCommandsAs((Func<Type, bool>)(t => Regex.IsMatch(t.Name, "Command(V\\d+)?$")))
            .DefiningEventsAs((Func<Type, bool>)(t => Regex.IsMatch(t.Name, "Event(V\\d+)?$")));


        var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
        services.AddSingleton(endpointInstance);
        services.AddSingleton<IMessageSession>(endpointInstance);
    })
    .Build();

SendEmailMessage(host.Services);

host.Start();

static void SendEmailMessage(IServiceProvider services)
{
    var messagingService = services.GetRequiredService<IMessageSession>();
    messagingService.Send(new SendEmailCommand("from.console@run.com"), CancellationToken.None).Wait();
}
