using Events;

namespace SmsApp.Senders
{
    public interface ISmsSender
    {
        Task<bool> Send(SmsEvent smsEvent);
    }
}
