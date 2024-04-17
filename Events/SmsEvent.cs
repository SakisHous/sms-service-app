using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public record SmsEvent
    {
        public string? MessageBody { get; init; }
        public string? SenderCountryCode { get; init; }
        public string? Sender { get; init; }
        public string? RecipientCountryCode { get; init; }
        public string? Recipient { get; init; }
        public string? Vendor { get; init; }
    }
}
