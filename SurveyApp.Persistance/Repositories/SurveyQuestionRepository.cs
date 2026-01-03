using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistance.Context;


namespace SurveyApp.Persistance.Repositories;

public class SurveyQuestionRepository : EfRepositoryBase<SurveyQuestion, int, BaseDbContext>, ISurveyQuestionRepository	
{
	public SurveyQuestionRepository(BaseDbContext context) : base(context)
	{
	}
}