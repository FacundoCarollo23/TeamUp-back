using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeamUp.DAL.Interfaces;
using TeamUp.BLL.contract;
using TeamUp.DTO;
using TeamUp.Model;

namespace TeamUp.DAL.Repository
{
    public class EventService : IEventService
    {
        private readonly IGenericRepository<Event> _EventRepository;
        private readonly IMapper _mapper;

        public EventService(IGenericRepository<Event> eventRepository, IMapper mapper)
        {
            _EventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<List<EventDTO>> List()
        {
            try
            {
                var queryEvent = await _EventRepository.Consult();
                var listEvent = queryEvent.Include(Country => Country.Country)
                    .Include(DifficultyLevel => DifficultyLevel.DifficultyLevel)
                    .Include(Activity => Activity.Activity)
                    .ToList();

                return _mapper.Map<List<EventDTO>>(listEvent);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<EventDTO>> ListRecent()
        {
            try
            {
                var queryEvent = await _EventRepository.Consult();
                var listEvent = queryEvent.Include(Country => Country.Country)
                    .Include(DifficultyLevel => DifficultyLevel.DifficultyLevel)
                    .Include(Activity => Activity.Activity)
                    .OrderByDescending(a => a.DateTime)
                    .ToList();

                return _mapper.Map<List<EventDTO>>(listEvent);
            }
            catch
            {
                throw;
            }
        }

        public async Task<EventDTO> Create(EventDTO model)
        {
            try
            {
                var eventCreate = await _EventRepository.Create(_mapper.Map<Event>(model));

                if (eventCreate.EventId == 0)
                    throw new TaskCanceledException("No se pudo crear");

                var query = await _EventRepository.Consult(u => u.EventId == eventCreate.EventId);

                return _mapper.Map<EventDTO>(eventCreate);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var EventFound = await _EventRepository.Obtain(u => u.EventId == id);

                if (EventFound == null)
                    throw new TaskCanceledException("El evento no existe");

                bool res = await _EventRepository.Delete(EventFound);

                if (!res)
                    throw new TaskCanceledException("No se pudo eliminar");

                return res;
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> Edit(EventDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<EventDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
