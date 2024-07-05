using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet("getall")]
		public IActionResult GetAll()
		{
			var result = _productService.GetList();
			if (result.IsSuccess)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		[HttpGet("getById")]
		public IActionResult GetById(int id)
		{
			var result = _productService.GetById(id);
			if (result.IsSuccess)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		[HttpPost("add")]
		public IActionResult Add(Product product)
		{
			var result = _productService.Add(product);
			if (result.IsSuccess)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		[HttpPost("update")]
		public IActionResult Update(Product product)
		{
			var result = _productService.Update(product);
			if (result.IsSuccess)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		[HttpPost("delete")]
		public IActionResult Delete(int id)
		{
			var result = _productService.DeleteById(id);
			if (result.IsSuccess)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		[HttpPost("bulkInsert")]
		public async Task<IActionResult> BulkInsert(List<Product> products)
		{
			var result = await _productService.BulkInsertAsync(products);
			if (result.IsSuccess)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		[HttpPost("bulkUpdate")]
		public async Task<IActionResult> BulkUpdate(List<Product> products)
		{
			var result = await _productService.BulkUpdateAsync(products);
			if (result.IsSuccess)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		[HttpPost("bulkDelete")]
		public async Task<IActionResult> BulkDelete(List<Product> products)
		{
			var result = await _productService.BulkDeleteAsync(products);
			if (result.IsSuccess)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
	}
}
