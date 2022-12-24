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
            _operationClaimService.Add(operationClaim);
            return Ok("Kayıt işlemi başarili");
        }
    }
}
