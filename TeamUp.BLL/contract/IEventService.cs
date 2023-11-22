using TeamUp.DTO;
using TeamUp.Model;

namespace TeamUp.BLL.contract
{
    public interface IEventService
    {
        Task<List<EventDTO>> List();
        Task<List<EventDTO>> ListRecent();
        Task<List<EventDTO>> ListFeatured();
        Task<List<EventDTO>> ListCreatedByUser(int userId);
        Task<List<EventDTO>> ListAcceptedByUser(int userId);
        Task<List<EventDTO>> GetById(int id);
        Task<EventDTO> Create(EventUserDTO model);
        Task<bool> Edit(EventDTO model);
        Task<bool> Delete(int id);
    }
}
