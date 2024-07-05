using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.RoleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _roleService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int roleId)
        {
            var result = _roleService.GetById(roleId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(RoleForAddDto roleForAddDto)
        {
            var result = _roleService.Add(roleForAddDto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Role role)
        {
            var result = _roleService.Update(role);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deleteById")]
        public IActionResult Delete(int roleId)
        {
            var result = _roleService.DeleteById(roleId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    }
}
