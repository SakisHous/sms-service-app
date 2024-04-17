namespace SmsApp.DTO
{
    public record SmsRequest
    {
        public string? MessageBody { get; init; }
        public string? SenderCountryCode { get; init; }
        public string? Sender { get; init; }
        public string? RecipientCountryCode { get; init; }
        public string? Recipient { get; init; }
    }
}
