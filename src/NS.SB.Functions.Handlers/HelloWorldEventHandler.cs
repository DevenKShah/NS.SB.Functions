using Microsoft.Extensions.Logging;
using NS.SB.Functions.Messages;

namespace NS.SB.Functions.Handlers;
public class HelloWorldEventHandler(ILogger<HelloWorldEventHandler> _logger) : IHandleMessages<HelloWorldEvent>
{
    public Task Handle(HelloWorldEvent message, IMessageHandlerContext context)
    {
        _logger.LogWarning(message.Message);
        return Task.CompletedTask;
    }
}
