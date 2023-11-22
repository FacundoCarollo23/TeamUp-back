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
        private readonly IGenericRepository<UsersEvent> _UsersEventRepository;

        public EventService(IGenericRepository<Event> eventRepository, IMapper mapper, IGenericRepository<UsersEvent> usersEventRepository)
        {
            _EventRepository = eventRepository;
            _mapper = mapper;
            _UsersEventRepository = usersEventRepository;
        }

        public async Task<List<EventDTO>> List()
        {
            try
            {
                var queryEvent = await _EventRepository.Consult();
                //var query = await _UsersEventRepository.Consult();


                var listEvent = queryEvent.Include(Country => Country.Country)
                    .Include(DifficultyLevel => DifficultyLevel.DifficultyLevel)
                    .Include(Activity => Activity.Activity)
                    //.Include(UsersEvent => UsersEvent.UsersEvents)
                    .ToList();

                //List<EventDTO> events = new List<EventDTO>();


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

        public async Task<List<EventDTO>> ListFeatured()
        {
            try
            {
                var queryEvent = await _EventRepository.Consult();
                var listEvent = queryEvent.Include(Country => Country.Country)
                    .Include(DifficultyLevel => DifficultyLevel.DifficultyLevel)
                    .Include(Activity => Activity.Activity)
                    .OrderBy(a => a.DifficultyLevel)
                    .ToList();

                return _mapper.Map<List<EventDTO>>(listEvent);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<EventDTO>> ListCreatedByUser(int userId)
        {
            IQueryable<UsersEvent> tbUsersEvents = await _UsersEventRepository.Consult(u => u.UserId == userId & u.RolId == false);
            IQueryable<Event> tbEvents = await _EventRepository.Consult();

            try
            {
                // Busco los eventos en los que el usuario figura como creador
                IQueryable<Event> tbEventsCreados = ( from e in tbEvents
                                                      join ue in tbUsersEvents on e.EventId equals ue.EventId
                                                      select e).AsQueryable();


                var EventsList = tbEventsCreados.Include(Country => Country.Country)
                    .Include(DifficultyLevel => DifficultyLevel.DifficultyLevel)
                    .Include(Activity => Activity.Activity)
                    .ToList();

                return _mapper.Map<List<EventDTO>>(EventsList);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<EventDTO>> ListAcceptedByUser(int userId)
        {
            IQueryable<UsersEvent> tbUsersEvents = await _UsersEventRepository.Consult(u => u.UserId == userId & u.RolId == true);
            IQueryable<Event> tbEvents = await _EventRepository.Consult();

            try
            {
                // Busco los eventos en los que el usuario figura como participante
                IQueryable<Event> tbEventsAceptados = (from e in tbEvents
                                                     join ue in tbUsersEvents on e.EventId equals ue.EventId
                                                     select e).AsQueryable();


                var EventsList = tbEventsAceptados.Include(Country => Country.Country)
                    .Include(DifficultyLevel => DifficultyLevel.DifficultyLevel)
                    .Include(Activity => Activity.Activity)
                    .ToList();

                return _mapper.Map<List<EventDTO>>(EventsList);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<EventDTO>> GetById(int id)
        {
            try
            {
                var queryUser = await _UsersEventRepository.Consult(u => u.EventId == id & u.RolId == false);

                var queryEvent = await _EventRepository.Consult(u => u.EventId == id);
                var eventFound = queryEvent.Include(Country => Country.Country)
                    .Include(DifficultyLevel => DifficultyLevel.DifficultyLevel)
                    .Include(Activity => Activity.Activity);
             
                if (eventFound == null)
                    throw new TaskCanceledException("El evento no existe");

                return _mapper.Map<List<EventDTO>>(eventFound);
            }
            catch
            {
                throw;
            }
        }

        public async Task<EventDTO> Create(EventUserDTO model)
        {
            try
            {
                var eventCreate = await _EventRepository.Create(_mapper.Map<Event>(model));

                if (eventCreate.EventId == 0)
                    throw new TaskCanceledException("No se pudo crear");

                var query = await _EventRepository.Consult(u => u.EventId == eventCreate.EventId);

                // Creo la asociación del Usuario y Evento en usersEvent
                var usersEventModel = new UsersContadorDTO();

                usersEventModel.EventId = eventCreate.EventId; // Evento generado
                usersEventModel.RolId = 0; // Creador
                usersEventModel.UserId = model.UserId;

                var usersEventReturn = await _UsersEventRepository.Create(_mapper.Map<UsersEvent>(usersEventModel));

                if (usersEventReturn.UserEventId == 0)
                    throw new TaskCanceledException("Error al crear asociación con el Usuario");

                // Agrego el usuario en una variable nueva para poder usar la estructura de EventDTO
                var responseModel = _mapper.Map<EventDTO>(eventCreate);
                responseModel.UserId = model.UserId;

                return responseModel;

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

        
    }
}
