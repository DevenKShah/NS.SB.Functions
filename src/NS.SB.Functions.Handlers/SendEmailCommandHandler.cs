using Microsoft.Extensions.Logging;
using NS.SB.Functions.Messages;

namespace NS.SB.Functions.Handlers;

public class SendEmailCommandHandler(ILogger<SendEmailCommandHandler> _logger) : IHandleMessages<SendEmailCommand>
{
    public Task Handle(SendEmailCommand message, IMessageHandlerContext context)
    {
        _logger.LogWarning("Send email command received for {Email} at {Time}", message.RecipientsAddress, DateTime.Now.ToShortTimeString());
        return Task.CompletedTask;
    }
}
