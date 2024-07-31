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

        public async Task<List<EventsCommentDTO>> List(int? eventId)
        {
            try
            {
                var queryEvent = await _EventsCommentRepository.Consult(ec => ec.EventId == eventId);

                if (eventId == 0)
                {
                    queryEvent = await _EventsCommentRepository.Consult();
                }

                var listEvent = queryEvent.Include(Event => Event.Event)
                    .Include(User => User.User)
                    .OrderBy(date => date.DateTime)
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

        public async Task<EventsCommentDTO> CreateComment(EventsCommentDTO model)
        {
            try
            {

                var commentCreate = await _EventsCommentRepository.Create(_mapper.Map<EventsComment>(model));

                if (commentCreate.EventCommentId == 0)
                    throw new TaskCanceledException("No se pudo crear");

                var query = await _EventsCommentRepository.Consult(u => u.EventCommentId == commentCreate.EventCommentId);

                // vuelco a una lista de un sólo registro los datos que faltan para exponerlos
                var listNewComment = query.Include(Event => Event.Event)
                    .Include(User => User.User)
                    .ToList();

                // devuelvo el mapeo completo del DTO
                return _mapper.Map<EventsCommentDTO>(listNewComment[0]);
            }
            catch
            {

                throw;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
