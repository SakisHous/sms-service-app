using Events;
using MassTransit;
using SmsApp.Factories;

namespace SmsApp.Consumers
{
    public class SmsVendorCYConsumer : IConsumer<SmsEvent>
    {
        private readonly ISmsSenderFactory _smsSenderFactory;

        public SmsVendorCYConsumer(ISmsSenderFactory smsSenderFactory)
        {
            _smsSenderFactory = smsSenderFactory;
        }

        public async Task Consume(ConsumeContext<SmsEvent> context)
        {
            var vendorCYSender = _smsSenderFactory.GetInstance("SmsVendorCY");

            if (vendorCYSender == null)
            {
                // Code should not be reached here
                return;
            }

            await vendorCYSender.Send(context.Message);
        }
    }
}
