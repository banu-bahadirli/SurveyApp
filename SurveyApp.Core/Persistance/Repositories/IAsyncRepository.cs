using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.Persistance.Repositories
{
	public interface IAsyncRepository<TEntity, TId> where TEntity : Entity<TId>
	{
		// Tek bir entity getir
		Task<TEntity?> GetAsync(
			Expression<Func<TEntity, bool>> predicate,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default);

		// Pagination'lı liste
		Task<Paginate<TEntity>> GetListAsync(
			Expression<Func<TEntity, bool>>? predicate = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
			int index = 0,
			int size = 10,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default);

		// Pagination olmadan, include ile ilişkili tablolar dahil
		Task<List<TEntity>> GetListNoPaginationAsync(
			Expression<Func<TEntity, bool>>? predicate = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default);

		// Any
		Task<bool> AnyAsync(
			Expression<Func<TEntity, bool>>? predicate = null,
			bool withDeleted = false,
			bool enableTracking = true,
			CancellationToken cancellationToken = default);

		// Add
		Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

		// Update
		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

		// Delete
		Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false, CancellationToken cancellationToken = default);
	}
}
