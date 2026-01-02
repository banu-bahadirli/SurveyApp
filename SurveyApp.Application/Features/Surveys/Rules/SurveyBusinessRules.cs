using SurveyApp.Application.Features.Surveys.Constants;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.CrossCuttingConcerns.Exceptions.Types;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Rules;

public class SurveyBusinessRules
{
	private readonly ISurveyRepository _surveyRepository;

	public SurveyBusinessRules(ISurveyRepository surveyRepository)
	{
		_surveyRepository = surveyRepository;
	}

	public async Task SurveyTitleCannotBeDuplicatedWhenInserted(string title)
	{
		Survey? result = await _surveyRepository.GetAsync(predicate: b => b.Title.ToLower() == title.ToLower());

		if (result != null)
		{
			throw new BusinessException(SurveyMessages.SurveyTitleExists);

		}
	}
}