using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NArcBackEnd.Business.Abstract;
using NArcBackEnd.Entities.Concrete;

namespace NArcBackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailParametersController : ControllerBase
    {
        private readonly IEmailParameterService _emailParameterService;

        public EmailParametersController(IEmailParameterService emailParameterService)
        {
            _emailParameterService = emailParameterService;
        }

        [HttpPost]
        public IActionResult Add(EmailParameter emailParameter)
        {
            var result = _emailParameterService.Add(emailParameter);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult Update(EmailParameter emailParameter)
        {
            var result = _emailParameterService.Update(emailParameter);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete(EmailParameter emailParameter)
        {
            var result = _emailParameterService.Delete(emailParameter);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var result = _emailParameterService.GetList();
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _emailParameterService.GetById(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
