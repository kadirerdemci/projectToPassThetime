using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private ILogService _logService;
        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpDelete("deleteall")]
        public IActionResult DeleteAll()
        {
            var result = _logService.DeleteAll();
            if (result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _logService.GetAll();
            if (result.Count > 0)
            {
                return Ok(result);
            }
       
            return BadRequest(result);
        }
    }
}
