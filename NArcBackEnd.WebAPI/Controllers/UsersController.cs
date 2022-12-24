using Microsoft.AspNetCore.Mvc;
using NArcBackEnd.Business.Abstract;
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
            return Ok(_userService.GetList());
        }
    }
}
