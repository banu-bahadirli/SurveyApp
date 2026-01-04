using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Features.AnswerTemplates.Dtos;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetSurveyQuestions;

public class GetSurveyQuestionsQuery : IRequest<List<GetSurveyQuestionsResponse>>
{
	public int SurveyId { get; set; }

	public class GetSurveyQuestionsQueryHandler : IRequestHandler<GetSurveyQuestionsQuery, List<GetSurveyQuestionsResponse>>
	{
		private readonly ISurveyQuestionRepository _surveyQuestionRepository;
		private readonly IMapper _mapper;

		public GetSurveyQuestionsQueryHandler(
			ISurveyQuestionRepository surveyQuestionRepository,
			IMapper mapper)
		{
			_surveyQuestionRepository = surveyQuestionRepository;
			_mapper = mapper;
		}

		public async Task<List<GetSurveyQuestionsResponse>> Handle(GetSurveyQuestionsQuery request, CancellationToken cancellationToken)
		{
			var surveyQuestions = await _surveyQuestionRepository.GetListNoPaginationAsync(
				predicate: sq => sq.SurveyId == request.SurveyId,
				include: q => q
					.Include(sq => sq.Question)
						.ThenInclude(q => q.AnswerTemplate)
							.ThenInclude(at => at.Options),
				enableTracking: false,
				cancellationToken: cancellationToken
			);

			var response = surveyQuestions.Select(sq => new GetSurveyQuestionsResponse
			{
				QuestionId = sq.QuestionId,
				QuestionText = sq.Question.Text,
				QuestionType = "MultipleChoice",
				Options = sq.Question.AnswerTemplate?.Options?
							.OrderBy(o => o.Order)
							.Select(o => _mapper.Map<AnswerOptionDto>(o))
							.ToList() ?? new List<AnswerOptionDto>()
			}).ToList();

			return response;
		}
	}

}
