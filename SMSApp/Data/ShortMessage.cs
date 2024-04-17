namespace SmsApp.Data;

public partial class ShortMessage
{
    public int MessagesId { get; set; }

    public string? MessageBody { get; set; }

    public string? SenderCountryCode { get; set; }

    public string? Sender { get; set; }

    public string? RecipientCountryCode { get; set; }

    public string? Recipient { get; set; }

    public string? Vendor { get; set; }
}
