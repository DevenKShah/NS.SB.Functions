using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NS.SB.Functions.Messages;
using NServiceBus;

namespace NS.SB.Sender.Functions;

public class PublishEventFunction(IFunctionEndpoint _functionEndpoint, ILogger<PublishEventFunction> logger)
{
    [Function("Function1")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        FunctionContext executionContext)
    {
        logger.LogInformation("C# Timer trigger function executed at: {Time}", DateTime.Now);

        HelloWorldEvent myEvent = new("Deven", DateTime.Now);

        await _functionEndpoint.Publish(myEvent, executionContext);

        return new OkResult();
    }
}
