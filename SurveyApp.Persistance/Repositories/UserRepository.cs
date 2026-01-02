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
	public class UserRepository : EfRepositoryBase<User, int, BaseDbContext>, IUserRepository
	{
		public UserRepository(BaseDbContext context) : base(context)
		{
		}

	}
}
