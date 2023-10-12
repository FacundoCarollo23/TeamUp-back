using Microsoft.EntityFrameworkCore;
using TeamUp.DAL.Interfaces;
using TeamUp.Model;

namespace TeamUp.DAL.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly TeamUpContext _context;

        public EventRepository(TeamUpContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            var posts = await _context.Events.ToListAsync();
            return posts;
        }
    }
}
