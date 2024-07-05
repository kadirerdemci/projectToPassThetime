using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.RoleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {

        private IRolePermissionService _rolePermissionService;

        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _rolePermissionService.GetList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int rolePermissionId)
        {
            var result = _rolePermissionService.GetById(rolePermissionId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Core.Entities.Concrete.RolePermission rolePermission)
        {
            var result = _rolePermissionService.Add(rolePermission);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Core.Entities.Concrete.RolePermission rolePermission)
        {
            var result = _rolePermissionService.Update(rolePermission);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deleteById")]
        public IActionResult Delete(int rolePermissionId)
        {
            var result = _rolePermissionService.DeleteById(rolePermissionId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("bulkDeleteAsync")]
		public async Task<IActionResult> BulkDeleteAsync(List<Core.Entities.Concrete.RolePermission> rolePermissions)
		{
			var result = await _rolePermissionService.BulkDeleteAsync(rolePermissions);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}
	}
}
