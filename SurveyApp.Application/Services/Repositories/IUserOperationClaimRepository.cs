using Microsoft.EntityFrameworkCore.Query;
using SurveyApp.Core.Persistance.Repositories;

using SurveyApp.Core.Security.Entities;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.Repositories
{
	public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim, int>
	{
		//Task<IList<UserOperationClaim>> GetAllByUserIdAsync(Guid userId,
		//	Func<IQueryable<UserOperationClaim>, IIncludableQueryable<UserOperationClaim, object>>? include = null,
		//	bool enableTracking = true,
		//	CancellationToken cancellationToken = default);
		Task<List<OperationClaim>> GetOperationClaimsByUserIdAsync(int userId);

	}

}
