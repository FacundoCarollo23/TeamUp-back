using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamUp.BLL.sinNombre;
using TeamUp.DTO;

namespace TeamUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var rsp = new Utility.Response<List<UserDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _userService.List();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var rsp = new Utility.Response<SesionDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _userService.ValidateData(login.Email, login.Password);
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

        public async Task<IActionResult> Create([FromBody] UserDTO user)
        {
            var rsp = new Utility.Response<UserDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _userService.Create(user);
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

        public async Task<IActionResult> Edit([FromBody] UserDTO user)
        {
            var rsp = new Utility.Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _userService.Edit(user);
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
                rsp.value = await _userService.Delete(id);
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
