using SmsApp.Senders;

namespace SmsApp.Factories
{
    public interface ISmsSenderFactory
    {
        ISmsSender GetInstance(string token);
    }
}
