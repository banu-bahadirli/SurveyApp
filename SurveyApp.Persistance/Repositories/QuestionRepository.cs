using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistance.Context;


namespace SurveyApp.Persistance.Repositories
{
	public class QuestionRepository : EfRepositoryBase<Question, int, BaseDbContext>, IQuestionRepository
	{
		public QuestionRepository(BaseDbContext context) : base(context)
		{
		}
	}
}
