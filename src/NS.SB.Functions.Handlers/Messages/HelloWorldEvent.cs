namespace NS.SB.Functions.Messages;
public record HelloWorldEvent(string Name, DateTime MessageTime) : IEvent
{
    public string Message => $"Hello {Name}! on {MessageTime.ToShortTimeString()}";
}
