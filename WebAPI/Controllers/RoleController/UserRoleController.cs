using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.RoleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {

        private IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userRoleService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int userRoleId)
        {
            var result = _userRoleService.GetById(userRoleId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(UserRoleForAddDto userRoleForAddDto)
        {
            var result = _userRoleService.Add(userRoleForAddDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(UserRole userRole)
        {
            var result = _userRoleService.Update(userRole);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deleteById")]
        public IActionResult Delete(int userRoleId)
        {
            var result = _userRoleService.DeleteById(userRoleId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
