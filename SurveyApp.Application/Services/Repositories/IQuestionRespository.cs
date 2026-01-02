using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Services.Repositories
{
	public interface IQuestionRespository : IAsyncRepository<Question, int>
	{
	}
}
