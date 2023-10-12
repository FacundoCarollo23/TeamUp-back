using Microsoft.AspNetCore.Mvc;
using TeamUp.DAL.Interfaces;

namespace TeamUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        public EventController(IEventRepository EventRepository)
        {
            _eventRepository = EventRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var posts = await _eventRepository.GetEvents();
            return Ok(posts);
        }
    }
}
