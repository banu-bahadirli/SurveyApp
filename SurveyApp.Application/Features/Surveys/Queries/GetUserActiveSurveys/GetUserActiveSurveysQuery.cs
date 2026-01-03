using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.Surveys.Queries.GetUserActiveSurveys
{
	public class GetUserActiveSurveysQuery : IRequest<List<GetUserActiveSurveyResponse>>
	{
		public int UserId { get; init; }

		public class GetUserActiveSurveysQueryHandler
			: IRequestHandler<GetUserActiveSurveysQuery, List<GetUserActiveSurveyResponse>>
		{
			private readonly IUserSurveyRepository _userSurveyRepository;
			private readonly IMapper _mapper;

			public GetUserActiveSurveysQueryHandler(
				IUserSurveyRepository userSurveyRepository,
				IMapper mapper)
			{
				_userSurveyRepository = userSurveyRepository;
				_mapper = mapper;
			}

			public async Task<List<GetUserActiveSurveyResponse>> Handle(
				GetUserActiveSurveysQuery request,
				CancellationToken cancellationToken)
			{
				var now = DateTime.UtcNow;

				// Kullanıcıya atanmış ve aktif olan anketleri çek
				var userSurveys = await _userSurveyRepository.GetListNoPaginationAsync(
					predicate: us =>
						us.UserId == request.UserId &&
						!us.IsCompleted &&
						us.DeletedDate == null &&
						us.Survey != null &&
						us.Survey.IsActive &&
						us.Survey.StartDate <= now &&
						us.Survey.EndDate >= now,
					include: q => q.Include(us => us.Survey),
					enableTracking: false,
					cancellationToken: cancellationToken
				);

				// Mapper ile response DTO’ya dönüştür
				var response = _mapper.Map<List<GetUserActiveSurveyResponse>>(userSurveys);
				return response;
			}
		}
	}
}
