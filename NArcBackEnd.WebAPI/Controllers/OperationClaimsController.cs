using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }


        [HttpPost]
        public IActionResult Add(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Add(operationClaim);
            if(result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult Update(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Update(operationClaim);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Delete(operationClaim);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        //[Authorize(Roles ="GetList")] // buradaki yapıyı program.csdeki authentication ve authorization yapısı ile bulmaktadır. Userdan claimslerine gidip oradan bakmaktadır yine.
        public IActionResult GetList()
        {
            var result = _operationClaimService.GetList();
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _operationClaimService.GetById(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
