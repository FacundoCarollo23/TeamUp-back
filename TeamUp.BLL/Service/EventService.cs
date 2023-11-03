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

        public Task<EventUserDTO> Create(EventUserDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(EventUserDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<EventDTO> GetById(int id)
        {
            throw new NotImplementedException();
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
    }
}
