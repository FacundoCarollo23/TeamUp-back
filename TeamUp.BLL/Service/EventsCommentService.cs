using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeamUp.BLL.contract;
using TeamUp.DAL.Interfaces;
using TeamUp.DTO;
using TeamUp.Model;

namespace TeamUp.BLL.Service
{
    public class EventsCommentService : IEventsCommentService
    {
        private readonly IGenericRepository<EventsComment> _EventsCommentRepository;
        private readonly IMapper _mapper;

        public EventsCommentService(IGenericRepository<EventsComment> eventsCommentRepository, IMapper mapper)
        {
            _EventsCommentRepository = eventsCommentRepository;
            _mapper = mapper;
        }

        public async Task<List<EventsCommentDTO>> List()
        {
            try
            {
                var queryEvent = await _EventsCommentRepository.Consult();
                var listEvent = queryEvent.Include(Event => Event.Event)
                    .Include(User => User.User)
                    .ToList();

                return _mapper.Map<List<EventsCommentDTO>>(listEvent);
            }
            catch
            {
                throw;
            }
        }

        public Task<EventsCommentDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
