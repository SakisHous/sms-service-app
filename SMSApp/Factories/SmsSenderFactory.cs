using SmsApp.Senders;

namespace SmsApp.Factories
{
    public class SmsSenderFactory : ISmsSenderFactory
    {
        private readonly IEnumerable<ISmsSender> _smsSenders;

        public SmsSenderFactory(IEnumerable<ISmsSender> smsSenders)
        {
            _smsSenders = smsSenders;
        }

        public ISmsSender GetInstance(string token)
        {
            return token switch
            {
                "SmsVendorGR" => this.GetService(typeof(SmsVendorGRSender)),
                "SmsVendorCY" => this.GetService(typeof(SmsVendorCYSender)),
                "SmsVendorRest" => this.GetService(typeof(SmsVendorRestSender)),
                _ => throw new InvalidOperationException()
            };
        }

        public ISmsSender GetService(Type type)
        {
            return _smsSenders.FirstOrDefault(x => x.GetType() == type)!;
        }
    }
}
