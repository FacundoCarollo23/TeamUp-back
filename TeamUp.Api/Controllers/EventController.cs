using Microsoft.AspNetCore.Mvc;
using TeamUp.BLL.contract;
using TeamUp.DTO;

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
    }
}
