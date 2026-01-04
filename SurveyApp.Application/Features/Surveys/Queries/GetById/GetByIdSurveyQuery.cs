using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.Surveys.Queries.GetById
{
	public class GetByIdSurveyQuery : IRequest<GetByIdSurveyResponse>
	{
		public int Id { get; set; }

		public class GetByIdSurveyQueryHandler : IRequestHandler<GetByIdSurveyQuery, GetByIdSurveyResponse>
		{
			private readonly ISurveyRepository _surveyRepository;
			private readonly IMapper _mapper;

			public GetByIdSurveyQueryHandler(
				ISurveyRepository surveyRepository,
				IMapper mapper)
			{
				_surveyRepository = surveyRepository;
				_mapper = mapper;
			}

			public async Task<GetByIdSurveyResponse> Handle(
				GetByIdSurveyQuery request,
				CancellationToken cancellationToken)
			{
				Survey? survey = await _surveyRepository.GetAsync(
					predicate: s => s.Id == request.Id,
					include: q => q
						.Include(s => s.UserSurveys)
						.Include(s => s.SurveyQuestions),
					withDeleted: false, 
					cancellationToken: cancellationToken
				);

				if (survey == null)
					return null!; 


				GetByIdSurveyResponse response = _mapper.Map<GetByIdSurveyResponse>(survey);
				response.UserIds = survey.UserSurveys.Select(us => us.UserId).ToList();
				response.QuestionIds = survey.SurveyQuestions.Select(sq => sq.QuestionId).ToList();
				return response;
			}
		}
	}
}
