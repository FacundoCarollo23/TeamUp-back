using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeamUp.DAL.Interfaces;
using TeamUp.BLL.contract;
using TeamUp.DTO;
using TeamUp.Model;
using System.Threading.Channels;

namespace TeamUp.DAL.Repository
{
    public class EventService : IEventService
    {
        private readonly IGenericRepository<Event> _EventRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<UsersEvent> _UsersEventRepository;
        private readonly IGenericRepository<User> _UserRepository;

        public EventService(IGenericRepository<Event> eventRepository, IMapper mapper, IGenericRepository<UsersEvent> usersEventRepository, IGenericRepository<User> userRepository)
        {
            _EventRepository = eventRepository;
            _mapper = mapper;
            _UsersEventRepository = usersEventRepository;
            _UserRepository = userRepository;
        }

        public async Task<List<EventDTO>> ListEvent()
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
                    .OrderByDescending(timeEvent => timeEvent.EventCreateDateTime)
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
                    .OrderByDescending(a => a.UserCount)
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
            IQueryable<UsersEvent> tbUsersEvents = await _UsersEventRepository.Consult(u => u.UserId == userId); // & u.RolId == true);
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
                    .OrderByDescending(DateTime => DateTime.EventDateTime)
                    .ToList();

                return _mapper.Map<List<EventDTO>>(EventsList);
            }
            catch
            {
                throw;
            }
        }

        //Modificarlo para que traiga un evento en vez de una lista
        public async Task<List<EventDTO>> GetByIdEvent(int id)
        {
            try
            {
                var queryUser = await _UsersEventRepository.Obtain(u => u.EventId == id & u.RolId == false);
                var dataUser = await _UserRepository.Obtain(u => u.UserId == queryUser.UserId);

                var queryEvent = await _EventRepository.Consult(u => u.EventId == id);
                var eventFound = queryEvent.Include(Country => Country.Country)
                    .Include(DifficultyLevel => DifficultyLevel.DifficultyLevel)
                    .Include(Activity => Activity.Activity);

                List<EventDTO> result = _mapper.Map<List<EventDTO>>(eventFound);

                result[0].UserId = queryUser.UserId;
                result[0].Alias = dataUser.Alias;

                if (eventFound == null)
                    throw new TaskCanceledException("El evento no existe");

                return _mapper.Map<List<EventDTO>>(result);
            }
            catch
            {
                throw;
            }
        }

        public async Task<EventDTO> CreateEvent(EventUserDTO model)
        {
            try
            {
                // Seteo en 1 el contador de usuarios para evento nuevo
                model.UserCount = 1;

                var newEvent = _mapper.Map<Event>(model);

                newEvent.EventCreateDateTime = DateTime.Now;

                var eventCreate = await _EventRepository.Create(newEvent);

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

        public async Task<bool> EditEvent(EventDTO model)
        {
            try
            {
                var EventModel = _mapper.Map<Event>(model);

                var EventFound = await _EventRepository.Obtain(u => u.EventId == EventModel.EventId);

                if (EventFound == null)
                    throw new TaskCanceledException("El evento no Existe");

                EventFound.EventName = EventModel.EventName;
                EventFound.EventDescription = EventModel.EventDescription;
                EventFound.DifficultyLevelId = EventModel.DifficultyLevelId;
                EventFound.ActivityId = EventModel.ActivityId;
                EventFound.CountryId = EventModel.CountryId;
                EventFound.City = EventModel.City;

                bool res = await _EventRepository.Edit(EventFound);

                if (!res)
                    throw new TaskCanceledException("No se pudo editar");

                return res;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteEvent(int id)
        {
            try
            {
                var eventFound = await _EventRepository.Obtain(u => u.EventId == id);
                var usersFound = await _UsersEventRepository.Consult(u => u.EventId == id);

                if (eventFound == null)
                    throw new TaskCanceledException("El evento no existe");

                var listEvent = usersFound.ToList();

                //for(int i = 0; i < listEvent.Count; i++)
                //{
                //    await _UsersEventRepository.Delete(listEvent[i]);
                //}

                bool res = await _EventRepository.Delete(eventFound);

                if (!res)
                    throw new TaskCanceledException("No se pudo eliminar");

                return res;
            }
            catch
            {
                throw;
            }
        }

        //public async Task<UsersContadorDTO> addEvent(UsersContadorDTO model)
        //{
        //    try
        //    {
        //        var eventParticipant = await _UsersEventRepository.Create(_mapper.Map<UsersEvent>(model));

        //        if (eventParticipant.UserEventId == 0)
        //            throw new TaskCanceledException("No se pudo crear");

        //        var query = await _UsersEventRepository.Consult(u => u.UserId == eventParticipant.UserId);

        //        return _mapper.Map<UsersContadorDTO>(eventParticipant);

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public async Task<UsersContadorDTO> addUserToEvent(int eventId, int userId)
        {
            try
            {
                // Creo el modelo para el nuevo participante del evento en USERS_EVENTS
                var eventParticipant = new UsersContadorDTO();
                eventParticipant.UserId = userId;
                eventParticipant.EventId = eventId;
                eventParticipant.RolId = 1;

                await _UsersEventRepository.Create(_mapper.Map<UsersEvent>(eventParticipant));

                // Agrego uno al contador de usuarios de este evento específico en EVENTS
                var Event = await _EventRepository.Obtain(u => u.EventId == eventId);

                if (Event == null)
                    throw new TaskCanceledException("Error al identificar el evento");

                Event.UserCount++;

                bool res = await _EventRepository.Edit(Event);

                if (!res)
                    throw new TaskCanceledException("No se pudo agregar el participante al evento");

                return eventParticipant;
            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> removeUserFromEvent(int eventId, int userId)
        {
            try
            {
                // Busco el usuario y lo elimino de USERS_EVENTS
                var userFound = await _UsersEventRepository.Obtain(u => u.EventId == eventId & u.UserId == userId);

                if (userFound == null)
                    throw new TaskCanceledException("El usuario no existe");

                bool res = await _UsersEventRepository.Delete(userFound);

                if (!res)
                    throw new TaskCanceledException("No se pudo eliminar");

                // Resto uno al contador de usuarios de este evento específico en EVENTS
                var Event = await _EventRepository.Obtain(u => u.EventId == eventId);

                if (Event == null)
                    throw new TaskCanceledException("Error al identificar el evento");

                Event.UserCount--;

                bool res_event = await _EventRepository.Edit(Event);

                if (!res_event)
                    throw new TaskCanceledException("No se pudo bajar al participante del evento");

                return res;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<EventDTO>> GetByWord(string word)
        {
            try
            {
                var queryEvent = await _EventRepository.Consult(u => u.EventName.Contains(word));


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
