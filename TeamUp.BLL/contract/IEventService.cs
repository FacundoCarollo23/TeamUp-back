using TeamUp.DTO;
using TeamUp.Model;

namespace TeamUp.BLL.contract
{
    public interface IEventService
    {
        Task<List<EventDTO>> List();
        Task<EventDTO> GetById(int id);
        Task<EventDTO> Create(EventDTO model);
        Task<bool> Edit(EventDTO model);
        Task<bool> Delete(int id);
    }
}
