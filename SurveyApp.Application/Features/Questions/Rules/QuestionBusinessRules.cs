using SurveyApp.Application.Features.AnswerTemplates.Constants;
using SurveyApp.Application.Features.Questions.Constants;
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

		#region  Şablon var mı ve cevap tipi dolu mu?
		public async Task<string?> AnswerTemplateMustExist(int answerTemplateId)
		{
			var template = await _answerTemplateRepository.GetAsync(
				at => at.Id == answerTemplateId
			);

			if (template == null)
				return QuestionMessages.SelectedTemplateNotFound;

			return null; 
		}
		#endregion
	}
}
