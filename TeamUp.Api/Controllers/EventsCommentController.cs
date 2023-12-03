using Microsoft.AspNetCore.Mvc;
using TeamUp.BLL.contract;
using TeamUp.DAL.Interfaces;
using TeamUp.DAL.Repository;
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
        public async Task<IActionResult> List(int userId)
        {
            var rsp = new Utility.Response<List<EventsCommentDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventsCommentService.List(userId);
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

        public async Task<IActionResult> Create([FromBody] EventsCommentDTO @comment)
        {
            var rsp = new Utility.Response<EventsCommentDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _eventsCommentService.CreateComment(@comment);
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
