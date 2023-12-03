using TeamUp.DTO;
using TeamUp.Model;

namespace TeamUp.BLL.contract
{
    public interface IEventService
    {
        Task<List<EventDTO>> ListEvent();
        Task<List<EventDTO>> ListRecent();
        Task<List<EventDTO>> ListFeatured();
        Task<List<EventDTO>> ListCreatedByUser(int userId);
        Task<List<EventDTO>> ListAcceptedByUser(int userId);
        Task<List<EventDTO>> GetByIdEvent(int id);
        Task<EventDTO> CreateEvent(EventUserDTO model);
        Task<bool> EditEvent(EventDTO model);
        Task<bool> DeleteEvent(int id);
        //Task<UsersContadorDTO> addEvent(UsersContadorDTO model);
        Task<UsersContadorDTO> addUserToEvent(int eventId, int userId);
        Task<bool> removeUserFromEvent(int eventId, int userId);
    }
}
