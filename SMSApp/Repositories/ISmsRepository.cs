using Events;
using SmsApp.Data;

namespace SmsApp.Repositories
{
    public interface ISmsRepository
    {
        Task<bool> AddSmsAsync(ShortMessage message);
    }
}
