namespace NS.SB.Functions.Messages;
public record HelloWorldEvent(string Name, DateTime MessageTime)
{
    public string Message => $"Hello {Name}! on {MessageTime.ToShortTimeString()}";
}
