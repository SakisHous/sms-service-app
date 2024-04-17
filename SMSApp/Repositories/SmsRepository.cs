using Microsoft.EntityFrameworkCore;
using SmsApp.Data;

namespace SmsApp.Repositories
{
    public class SmsRepository : ISmsRepository
    {
        private readonly ShortMessagesDbContext _context;
        private readonly DbSet<ShortMessage> _table;

        public SmsRepository(ShortMessagesDbContext context)
        {
            _context = context;
            _table = _context.Set<ShortMessage>();
        }

        public async Task<bool> AddSmsAsync(ShortMessage message)
        {
            await _table.AddAsync(message);

            return _context.SaveChanges() > 0;
        }
    }
}
