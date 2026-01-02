using SurveyApp.Application.Features.Surveys.Constants;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.CrossCuttingConcerns.Exceptions.Types;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.Questions.Rules
{
	public class QuestionBusinessRules
	{
		private readonly IAnswerTemplateRepository _answerTemplateRepository;

		public QuestionBusinessRules(IAnswerTemplateRepository answerTemplateRepository)
		{
			_answerTemplateRepository = answerTemplateRepository;
		}

		public async Task AnswerTemplateMustExist(int answerTemplateId)
		{
			var exists = await _answerTemplateRepository.AnyAsync(
				at => at.Id == answerTemplateId
			);

			if (!exists)
				throw new BusinessException("Seçilen cevap şablonu bulunamadı");
		}
	}
}
