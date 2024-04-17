using Events;
using MassTransit;
using SmsApp.Factories;

namespace SmsApp.Consumers
{
    public class SmsVendorGRConsumer : IConsumer<SmsEvent>
    {
        private readonly ISmsSenderFactory _smsSenderFactory;

        public SmsVendorGRConsumer(ISmsSenderFactory smsSenderFactory)
        {
            _smsSenderFactory = smsSenderFactory;
        }

        public async Task Consume(ConsumeContext<SmsEvent> context)
        {
            var vendorGRSender = _smsSenderFactory.GetInstance("SmsVendorGR");

            if (vendorGRSender == null)
            {
                // Code should not be reached here
                return;
            }

            await vendorGRSender.Send(context.Message);
        }
    }
}
