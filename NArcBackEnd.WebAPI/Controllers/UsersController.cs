using Microsoft.AspNetCore.Mvc;
using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Entities.Concrete;
using NArcBackEnd.Entities.Dto;

namespace NArcBackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var result = _userService.GetList();
            if(result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("GetByEmail")]
        public IActionResult GetByEmail(string email)
        {
            //var result = _userService.GetByEmail(email);
            //if (result.Success) return Ok(result);
            //return BadRequest(result);
            return Ok(_userService.GetByEmail(email));
        }

        [HttpPut]
        public IActionResult Update(User user)
        {
            var result = _userService.Update(user);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete(User user)
        {
            var result = _userService.Delete(user);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var result = _userService.ChangePassword(userChangePasswordDto);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
