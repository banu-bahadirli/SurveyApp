using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetUserActiveSurveys;

public class GetUserActiveSurveysQuery : IRequest<List<GetUserActiveSurveyResponse>>
{
	public int UserId { get; init; }

	public class GetUserActiveSurveysQueryHandler
		: IRequestHandler<GetUserActiveSurveysQuery, List<GetUserActiveSurveyResponse>>
	{
		private readonly IAsyncRepository<UserSurvey, int> _userSurveyRepository;
		private readonly IMapper _mapper;

		public GetUserActiveSurveysQueryHandler(
			IAsyncRepository<UserSurvey, int> userSurveyRepository,
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

			// Kullanıcıya atanmış aktif anketleri çek
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
