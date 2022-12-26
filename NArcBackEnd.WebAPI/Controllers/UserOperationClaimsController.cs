using Microsoft.AspNetCore.Mvc;
using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var result = _userOperationClaimService.GetList();
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _userOperationClaimService.GetById(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult Add(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimService.Add(userOperationClaim);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult Update(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimService.Update(userOperationClaim);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimService.Delete(userOperationClaim);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
