using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
	public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
		where TEntity : class, IEntity, new()
		where TContext : DbContext, new()
	{
		public TEntity Get(Expression<Func<TEntity, bool>> filter)
		{
			using TContext context = new TContext();
			return context.Set<TEntity>().SingleOrDefault(filter);
		}

		public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
		{
			using TContext context = new TContext();
			return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
		}

		public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
		{
			using TContext context = new TContext();
			return filter == null
				? context.Set<TEntity>().ToList()
				: context.Set<TEntity>().Where(filter).ToList();
		}

		public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
		{
			using TContext context = new TContext();
			return filter == null
				? await context.Set<TEntity>().ToListAsync()
				: await context.Set<TEntity>().Where(filter).ToListAsync();
		}

		public void Add(TEntity entity)
		{
			using TContext context = new TContext();
			var addedEntity = context.Entry(entity);
			addedEntity.State = EntityState.Added;
			context.SaveChanges();
		}

		public async Task AddAsync(TEntity entity)
		{
			using TContext context = new TContext();
			var addedEntity = context.Entry(entity);
			addedEntity.State = EntityState.Added;
			await context.SaveChangesAsync();
		}

		public void Update(TEntity entity)
		{
			using TContext context = new TContext();
			var updatedEntity = context.Entry(entity);
			updatedEntity.State = EntityState.Modified;
			context.SaveChanges();
		}

		public async Task UpdateAsync(TEntity entity)
		{
			using TContext context = new TContext();
			var updatedEntity = context.Entry(entity);
			updatedEntity.State = EntityState.Modified;
			await context.SaveChangesAsync();
		}

		public void Delete(TEntity entity)
		{
			using TContext context = new TContext();
			var deletedEntity = context.Entry(entity);
			deletedEntity.State = EntityState.Deleted;
			context.SaveChanges();
		}

		public async Task DeleteAsync(TEntity entity)
		{
			using TContext context = new TContext();
			var deletedEntity = context.Entry(entity);
			deletedEntity.State = EntityState.Deleted;
			await context.SaveChangesAsync();
		}

		public int Count(Expression<Func<TEntity, bool>> filter = null)
		{
			using TContext context = new TContext();
			return filter == null
				? context.Set<TEntity>().Count()
				: context.Set<TEntity>().Count(filter);
		}

		public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
		{
			using TContext context = new TContext();
			return filter == null
				? await context.Set<TEntity>().CountAsync()
				: await context.Set<TEntity>().CountAsync(filter);
		}

		public void BulkInsert(List<TEntity> entities)
		{
			using TContext context = new TContext();
			context.Set<TEntity>().AddRange(entities);
			context.SaveChanges();
		}

		public async Task BulkInsertAsync(List<TEntity> entities)
		{
			using TContext context = new TContext();
			await context.Set<TEntity>().AddRangeAsync(entities);
			await context.SaveChangesAsync();
		}

		public void BulkUpdate(List<TEntity> entities)
		{
			using TContext context = new TContext();
			context.Set<TEntity>().UpdateRange(entities);
			context.SaveChanges();
		}

		public async Task BulkUpdateAsync(List<TEntity> entities)
		{
			using TContext context = new TContext();
			context.Set<TEntity>().UpdateRange(entities);
			await context.SaveChangesAsync();
		}

		public void BulkDelete(List<TEntity> entities)
		{
			using TContext context = new TContext();
			context.Set<TEntity>().RemoveRange(entities);
			context.SaveChanges();
		}

		public async Task BulkDeleteAsync(List<TEntity> entities)
		{
			using TContext context = new TContext();
			context.Set<TEntity>().RemoveRange(entities);
			await context.SaveChangesAsync();
		}
	}
}
