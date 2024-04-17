using Events;
using MassTransit;
using SmsApp.Factories;
using SmsApp.Senders;

namespace SmsApp.Consumers
{
    public class SmsVendorRestConsumer : IConsumer<SmsEvent>
    {
        private readonly ISmsSenderFactory _smsSenderFactory;

        public SmsVendorRestConsumer(ISmsSenderFactory smsSenderFactory)
        {
            _smsSenderFactory = smsSenderFactory;
        }

        public async Task Consume(ConsumeContext<SmsEvent> context)
        {
            var vendorRestSender = _smsSenderFactory.GetInstance("SmsVendorRest");

            if (vendorRestSender == null)
            {
                // Code should not be reached here
                return;
            }

            await vendorRestSender.Send(context.Message);
        }
    }
}
