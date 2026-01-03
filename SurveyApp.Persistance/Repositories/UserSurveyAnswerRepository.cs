using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Persistance.Repositories
{ 
	public class UserSurveyAnswerRepository : EfRepositoryBase<UserSurveyAnswer, int, BaseDbContext>, IUserSurveyAnswerRepository
	{
		public UserSurveyAnswerRepository(BaseDbContext context) : base(context)
		{
		}

	}
}
