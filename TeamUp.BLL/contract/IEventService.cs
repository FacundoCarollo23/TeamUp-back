using TeamUp.DTO;
using TeamUp.Model;

namespace TeamUp.DAL.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDTO>> List();
        Task<EventDTO> GetById(int id);
        Task<EventUserDTO> Create(EventUserDTO model);
        Task<bool> Edit(EventUserDTO model);
        Task<bool> Delete(int id);
    }
}
