using AutoMapper;
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
                rsp.value = await _eventService.List();
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
        [Route("GetById/{id:int}")]
        
        public async Task<IActionResult> GetById(int id)
        {
            var rsp = new Utility.Response<EventDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventService.GetById(id);
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
                rsp.value = await _eventService.Create(@event);
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
                rsp.value = await _eventService.Delete(id);
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
