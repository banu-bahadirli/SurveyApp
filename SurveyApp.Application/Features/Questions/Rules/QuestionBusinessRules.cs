using SurveyApp.Application.Features.Questions.Constants;
using SurveyApp.Application.Services.Repositories;

namespace SurveyApp.Application.Features.Questions.Rules
{
	public class QuestionBusinessRules
	{
		private readonly IAnswerTemplateRepository _answerTemplateRepository;
		private readonly ISurveyQuestionRepository _surveyQuestionRepository;

		public QuestionBusinessRules(
			IAnswerTemplateRepository answerTemplateRepository,
			ISurveyQuestionRepository surveyQuestionRepository)
		{
			_answerTemplateRepository = answerTemplateRepository;
			_surveyQuestionRepository = surveyQuestionRepository;
		}


		#region Soru ankette kullanılıyorsa silinemez
		public async Task<string?> QuestionCannotBeDeletedIfUsedInSurvey(int questionId)
		{
			var isUsed = await _surveyQuestionRepository.AnyAsync(
				sq => sq.QuestionId == questionId
			);

			if (isUsed)
				return QuestionMessages.QuestionUsedInSurveyCannotBeDeleted;

			return null;
		}
		#endregion
	}
}
