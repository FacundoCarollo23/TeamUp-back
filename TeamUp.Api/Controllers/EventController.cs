using Microsoft.AspNetCore.Mvc;
using TeamUp.BLL.contract;
using TeamUp.BLL.Service;
using TeamUp.DAL.Interfaces;
using TeamUp.DTO;
using TeamUp.Model;

namespace TeamUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {

        private readonly IEventService _eventService;
        public EventController(IEventService EventService)
        {
            _eventService = EventService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var rsp = new Utility.Response<List<EventDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.ListEvent();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("ListRecent")]
        public async Task<IActionResult> ListRecent()
        {
            var rsp = new Utility.Response<List<EventDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.ListRecent();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("ListFeatured")]
        public async Task<IActionResult> ListFeatured()
        {
            var rsp = new Utility.Response<List<EventDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.ListFeatured();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("ListCreatedByUser/{userId:int}")]
        public async Task<IActionResult> ListCreatedByUser(int userId)
        {
            var rsp = new Utility.Response<List<EventDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.ListCreatedByUser(userId);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("ListAcceptedByUser/{userId:int}")]
        public async Task<IActionResult> ListAcceptedByUser(int userId)
        {
            var rsp = new Utility.Response<List<EventDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.ListAcceptedByUser(userId);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("GetByWord/{word}")]
        public async Task<IActionResult> GetByWord(string word)
        {
            var rsp = new Utility.Response<List<EventDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.GetByWord(word);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }


        [HttpGet]
        [Route("GetById/{id:int}")]
        
        public async Task<IActionResult> GetById(int id)
        {
            var rsp = new Utility.Response<List<EventDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.GetByIdEvent(id);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> Create([FromBody] EventUserDTO @event)
        {
            var rsp = new Utility.Response<EventDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.CreateEvent(@event);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPut]
        [Route("Edit")]

        public async Task<IActionResult> Edit([FromBody] EventDTO eventEdit)
        {
            var rsp = new Utility.Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.EditEvent(eventEdit);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]

        public async Task<IActionResult> Delete(int id)
        {
            var rsp = new Utility.Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.DeleteEvent(id);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        //[HttpPost]
        //[Route("Add")]

        //public async Task<IActionResult> Add([FromBody] UsersContadorDTO user)
        //{
        //    var rsp = new Utility.Response<UsersContadorDTO>();

        //    try
        //    {
        //        rsp.status = true;
        //        rsp.value = await _eventService.addEvent(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        rsp.status = false;
        //        rsp.msg = ex.Message;
        //    }

        //    return Ok(rsp);
        //}

        [HttpPost]
        [Route("addToEvent/{eventId:int}/{userId:int}")]

        public async Task<IActionResult> Add(int eventId, int userId)
        {
            var rsp = new Utility.Response<UsersContadorDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.addUserToEvent(eventId, userId);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpDelete]
        [Route("RemoveFromEvent/{eventId:int}/{userId:int}")]

        public async Task<IActionResult> Remove(int eventId, int userId)
        {
            var rsp = new Utility.Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.removeUserFromEvent(eventId, userId);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }
    }
}
