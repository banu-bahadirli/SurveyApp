using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.Persistance.Repositories
{
	public class EfRepositoryBase<TEntity, TId, TContext> : IAsyncRepository<TEntity, TId>
		where TEntity : Entity<TId>
		where TContext : DbContext
	{
		protected readonly TContext Context;

		public EfRepositoryBase(TContext context)
		{
			Context = context;
		}

		public async Task<TEntity?> GetAsync(
			Expression<Func<TEntity, bool>> predicate,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default)
		{
			IQueryable<TEntity> queryable = Context.Set<TEntity>();

			if (!enableTracking)
				queryable = queryable.AsNoTracking();

			if (include != null)
				queryable = include(queryable);

			if (!withDeleted)
				queryable = queryable.Where(x => x.DeletedDate == null);

			return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
		}

		public async Task<Paginate<TEntity>> GetListAsync(
			Expression<Func<TEntity, bool>>? predicate = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
			int index = 0,
			int size = 10,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default)
		{
			IQueryable<TEntity> queryable = Context.Set<TEntity>();

			if (!enableTracking)
				queryable = queryable.AsNoTracking();

			if (include != null)
				queryable = include(queryable);

			if (!withDeleted)
				queryable = queryable.Where(x => x.DeletedDate == null);

			if (predicate != null)
				queryable = queryable.Where(predicate);

			if (orderBy != null)
				queryable = orderBy(queryable);

			var totalItems = await queryable.CountAsync(cancellationToken);
			var items = await queryable.Skip(index * size).Take(size).ToListAsync(cancellationToken);

			return new Paginate<TEntity>
			{
				Items = items,
				PageNumber = index + 1,
				PageSize = size,
				TotalItems = totalItems,
				TotalPages = (int)Math.Ceiling(totalItems / (double)size)
			};
		}

		public async Task<List<TEntity>> GetListNoPaginationAsync(
			Expression<Func<TEntity, bool>>? predicate = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default)
		{
			IQueryable<TEntity> queryable = Context.Set<TEntity>();

			if (!enableTracking)
				queryable = queryable.AsNoTracking();

			if (include != null)
				queryable = include(queryable);

			if (!withDeleted)
				queryable = queryable.Where(x => x.DeletedDate == null);

			if (predicate != null)
				queryable = queryable.Where(predicate);

			if (orderBy != null)
				queryable = orderBy(queryable);

			return await queryable.ToListAsync(cancellationToken);
		}

		public async Task<bool> AnyAsync(
			Expression<Func<TEntity, bool>>? predicate = null,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default)
		{
			IQueryable<TEntity> queryable = Context.Set<TEntity>();

			if (!enableTracking)
				queryable = queryable.AsNoTracking();

			if (!withDeleted)
				queryable = queryable.Where(x => x.DeletedDate == null);

			if (predicate != null)
				queryable = queryable.Where(predicate);

			return await queryable.AnyAsync(cancellationToken);
		}

		public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
			await Context.SaveChangesAsync(cancellationToken);
			return entity;
		}

		public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			entity.UpdatedDate = DateTime.UtcNow;
			Context.Set<TEntity>().Update(entity);
			await Context.SaveChangesAsync(cancellationToken);
			return entity;
		}

		public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false, CancellationToken cancellationToken = default)
		{
			if (!permanent)
			{
				entity.DeletedDate = DateTime.UtcNow;
				Context.Set<TEntity>().Update(entity);
			}
			else
			{
				Context.Set<TEntity>().Remove(entity);
			}

			await Context.SaveChangesAsync(cancellationToken);
			return entity;
		}

	}
}
