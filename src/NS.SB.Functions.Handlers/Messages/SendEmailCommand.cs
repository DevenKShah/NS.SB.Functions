namespace NS.SB.Functions.Messages;
public class SendEmailCommand
{
    public string RecipientsAddress { get; }

    public SendEmailCommand(string recipientsAddress)
    {
        RecipientsAddress = recipientsAddress;
    }
}
