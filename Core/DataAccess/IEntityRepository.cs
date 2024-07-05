using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess
{
	public interface IEntityRepository<T> where T : class, IEntity, new()
	{
		T Get(Expression<Func<T, bool>> filter);
		Task<T> GetAsync(Expression<Func<T, bool>> filter);
		List<T> GetList(Expression<Func<T, bool>> filter = null);
		Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null);
		void Add(T entity);
		Task AddAsync(T entity);
		void Update(T entity);
		Task UpdateAsync(T entity);
		void Delete(T entity);
		Task DeleteAsync(T entity);
		int Count(Expression<Func<T, bool>> filter = null);
		Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
		void BulkInsert(List<T> entities);
		Task BulkInsertAsync(List<T> entities);
		void BulkUpdate(List<T> entities);
		Task BulkUpdateAsync(List<T> entities);
		void BulkDelete(List<T> entities);
		Task BulkDeleteAsync(List<T> entities);
	}
}