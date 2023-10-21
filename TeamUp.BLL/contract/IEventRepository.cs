using TeamUp.Model;

namespace TeamUp.DAL.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents();
    }
}
