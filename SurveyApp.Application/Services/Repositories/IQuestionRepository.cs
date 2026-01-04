using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Services.Repositories
{
	public interface IQuestionRepository : IAsyncRepository<Question, int>
	{
	}
}
