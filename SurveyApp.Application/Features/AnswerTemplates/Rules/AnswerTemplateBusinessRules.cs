using SurveyApp.Application.Features.AnswerTemplates.Constants;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.AnswerTemplates.Rules;

public class AnswerTemplateBusinessRules
{
	private readonly IAnswerTemplateRepository _answerTemplateRepository;
	private readonly IQuestionRepository _questionRepository;

	public AnswerTemplateBusinessRules(
		IAnswerTemplateRepository answerTemplateRepository,
		IQuestionRepository questionRepository)
	{
		_answerTemplateRepository = answerTemplateRepository;
		_questionRepository = questionRepository;
	}

    #region  Şık sayısı 2-4 arası mı?
	public Task<string?> OptionCountMustBeBetween2And4(int optionCount)
	{
		if (optionCount < 2 || optionCount > 4)
			return Task.FromResult<string?>(AnswerTemplateMessages.AnswerTemplateOptioncount);

		return Task.FromResult<string?>(null); 
	}
	#endregion

	#region Şık sayısı ile seçenek sayısı uyumlu mu?
	public Task<string?> OptionCountMustMatchOptions(int optionCount, int optionsCount)
	{
		if (optionCount != optionsCount)
			return Task.FromResult<string?>(AnswerTemplateMessages.AnswerTemplateOptionMatch);

		return Task.FromResult<string?>(null); // Geçerli
	}
	#endregion

    #region Silinebilir mi? (Sorularda kullanılmış mı?)
	public async Task<string?> CanAnswerTemplateBeDeleted(int answerTemplateId)
	{
		bool isUsed = await _questionRepository.AnyAsync(
			q => q.AnswerTemplateId == answerTemplateId
		);

		if (isUsed)
			return AnswerTemplateMessages.AnswerTemplateInUsed;

		return null;
	}
	#endregion
}
