using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistance.Context;


namespace SurveyApp.Persistance.Repositories
{
	public class QuestionRespository : EfRepositoryBase<Question, int, BaseDbContext>, IQuestionRespository
	{
		public QuestionRespository(BaseDbContext context) : base(context)
		{
		}
	}
}
