using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Persistance.Repositories;

using SurveyApp.Core.Security.Entities;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Persistance.Repositories
{
	public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, int, BaseDbContext>, IUserOperationClaimRepository
	{
		public UserOperationClaimRepository(BaseDbContext context) : base(context)
		{
		}

		public async Task<List<OperationClaim>> GetOperationClaimsByUserIdAsync(int userId)
		{
			return await Context.UserOperationClaims
				.Where(uoc => uoc.UserId == userId)
				.Include(uoc => uoc.OperationClaim)
				.Select(uoc => uoc.OperationClaim)
				.ToListAsync();
		}


		//public async Task<IList<UserOperationClaim>> GetAllByUserIdAsync(Guid userId,
		//				Func<IQueryable<UserOperationClaim>, IIncludableQueryable<UserOperationClaim, object>>? include = null,
		//				bool enableTracking = true,
		//				CancellationToken cancellationToken = default)
		//{
		//	IQueryable<UserOperationClaim> query = Context.UserOperationClaims.AsQueryable();

		//	if (!enableTracking)
		//		query = query.AsNoTracking();

		//	if (include != null)
		//		query = include(query);

		//	query = query.Where(uc => uc.UserId == userId);

		//	return await query.ToListAsync(cancellationToken);
		//}


	}
}
