using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.RoleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {

        private IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("getall")]
        [HttpLogging(loggingFields: HttpLoggingFields.Duration)]
        public IActionResult GetAll()
        {
            var result = _permissionService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getById")]
        [HttpLogging(loggingFields: HttpLoggingFields.RequestBody)]

        public IActionResult GetById(int permissionId)
        {
            var result = _permissionService.GetById(permissionId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(PermissionAddDto permissionAddDto)
        {
            var result = _permissionService.Add(permissionAddDto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Permission permission)
        {
            var result = _permissionService.Update(permission);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deleteById")]
        public IActionResult Delete(int permissionId)
        {
            var result = _permissionService.Delete(permissionId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deleteAll")]
        public IActionResult DeleteAll()
        {
            var result = _permissionService.DeleteAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("count")]
		public IActionResult Count()
		{
			var result = _permissionService.Count();
			return Ok(result);
		}
	}
}
