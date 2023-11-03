using Microsoft.AspNetCore.Mvc;
using TeamUp.BLL.contract;
using TeamUp.DAL.Interfaces;
using TeamUp.DTO;

namespace TeamUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsCommentController : Controller
    {
        private readonly IEventsCommentService _eventsCommentService;
        public EventsCommentController(IEventsCommentService EventsCommentService)
        {
            _eventsCommentService = EventsCommentService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var rsp = new Utility.Response<List<EventsCommentDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventsCommentService.List();
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
