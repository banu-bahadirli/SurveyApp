using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.CrossCuttingConcerns.Exceptions.Types;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.AnswerTemplates.Rules;

public class AnswerTemplateBusinessRules
{
	private readonly IAnswerTemplateRepository _answerTemplateRepository;

	public AnswerTemplateBusinessRules(IAnswerTemplateRepository answerTemplateRepositor)
	{
		_answerTemplateRepository = answerTemplateRepositor;
	}

	public Task OptionCountMustBeBetween2And4(int optionCount)
	{
		if (optionCount < 2 || optionCount > 4)
			throw new BusinessException("Şık sayısı 2 ile 4 arasında olmalıdır.");

		return Task.CompletedTask;
	}

	public Task OptionCountMustMatchOptions(int optionCount, int optionsCount)
	{
		if (optionCount != optionsCount)
			throw new BusinessException("Şık sayısı ile girilen seçenekler uyuşmuyor.");

		return Task.CompletedTask;
	}
}