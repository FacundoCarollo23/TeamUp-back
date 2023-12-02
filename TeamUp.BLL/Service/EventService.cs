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

        public EventService(IGenericRepository<Event> eventRepository, IMapper mapper, IGenericRepository<UsersEvent> usersEventRepository)
        {
            _EventRepository = eventRepository;
            _mapper = mapper;
            _UsersEventRepository = usersEventRepository;
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
                var queryEvent = await _EventRepository.Consult(u => u.DateTime >= DateTime.Now);
                var listEvent = queryEvent.Include(Country => Country.Country)
                    .Include(DifficultyLevel => DifficultyLevel.DifficultyLevel)
                    .Include(Activity => Activity.Activity)
                    .OrderBy(a => a.DateTime)
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

        public async Task<List<EventDTO>> GetByIdEvent(int id)
        {
            try
            {
                var queryUser = await _UsersEventRepository.Obtain(u => u.EventId == id & u.RolId == false);

                var queryEvent = await _EventRepository.Consult(u => u.EventId == id);
                var eventFound = queryEvent.Include(Country => Country.Country)
                    .Include(DifficultyLevel => DifficultyLevel.DifficultyLevel)
                    .Include(Activity => Activity.Activity);

                List<EventDTO> result = _mapper.Map<List<EventDTO>>(eventFound);

                result[0].UserId = queryUser.UserId;

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

                for(int i = 0; i < listEvent.Count; i++)
                {
                    await _UsersEventRepository.Delete(listEvent[i]);
                }

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

        public async Task<UsersContadorDTO> addEvent(int eventId, int userId)
        {
            try
            {
                var eventParticipant = new UsersContadorDTO();
                eventParticipant.UserId = userId;
                eventParticipant.EventId = eventId;
                eventParticipant.RolId = 1;

                await _UsersEventRepository.Create(_mapper.Map<UsersEvent>(eventParticipant));

                return eventParticipant;
            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> removeEvent(int eventId, int userId)
        {
            try
            {
                var userFound = await _UsersEventRepository.Obtain(u => u.EventId == eventId & u.UserId == userId);

                if (userFound == null)
                    throw new TaskCanceledException("El usuario no existe");

                bool res = await _UsersEventRepository.Delete(userFound);

                if (!res)
                    throw new TaskCanceledException("No se pudo eliminar");

                return res;
            }
            catch
            {
                throw;
            }
        }
    }
}
