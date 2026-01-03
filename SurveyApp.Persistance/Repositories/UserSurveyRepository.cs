using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Core.Security.Entities;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistance.Context;


namespace SurveyApp.Persistance.Repositories
{
	public class UserSurveyRepository : EfRepositoryBase<UserSurvey, int, BaseDbContext>, IUserSurveyRepository
	{
		public UserSurveyRepository(BaseDbContext context) : base(context)
		{
		}

	}
}
