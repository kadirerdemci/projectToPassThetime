using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IProductService
	{
		IDataResult<Product> GetById(int productId);
		IDataResult<List<Product>> GetList();
		IResult Add(Product product);
		IResult Update(Product product);
		IResult DeleteById(int productId);
		Task<IResult> BulkDeleteAsync(List<Product> products);
		Task<IResult> BulkInsertAsync(List<Product> products);
		Task<IResult> BulkUpdateAsync(List<Product> products);
	}
}