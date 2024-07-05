using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
	public class ProductManager : IProductService
	{
		private readonly IProductDal _productDal;

		public ProductManager(IProductDal productDal)
		{
			_productDal = productDal;
		}
		[CacheRemoveAspect("deleteremove")]
		public IDataResult<Product> GetById(int productId)
		{
			var product = _productDal.Get(p => p.ProductId == productId);
			if (product != null)
			{
				return new SuccessDataResult<Product>(product);
			}
			return new ErrorDataResult<Product>("Product not found");
		}

		[CacheAspect]
		public IDataResult<List<Product>> GetList()
		{
			var products = _productDal.GetList();
			return new SuccessDataResult<List<Product>>(products);
		}

		public IResult Add(Product product)
		{
			_productDal.Add(product);
			return new SuccessResult("Product added successfully");
		}

		public IResult Update(Product product)
		{
			_productDal.Update(product);
			return new SuccessResult("Product updated successfully");
		}

		public IResult DeleteById(int productId)
		{
			var product = _productDal.Get(p => p.ProductId == productId);
			if (product != null)
			{
				_productDal.Delete(product);
				return new SuccessResult("Product deleted successfully");
			}
			return new ErrorResult("Product not found");
		}

		public async Task<IResult> BulkDeleteAsync(List<Product> products)
		{
			await _productDal.BulkDeleteAsync(products);
			return new SuccessResult("Products deleted successfully");
		}

		public async Task<IResult> BulkInsertAsync(List<Product> products)
		{
			await _productDal.BulkInsertAsync(products);
			return new SuccessResult("Products inserted successfully");
		}

		public async Task<IResult> BulkUpdateAsync(List<Product> products)
		{
			await _productDal.BulkUpdateAsync(products);
			return new SuccessResult("Products updated successfully");
		}
	}
}
