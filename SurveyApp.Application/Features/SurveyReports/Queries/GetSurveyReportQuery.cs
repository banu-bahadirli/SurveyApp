using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Features.SurveyReports.Dtos;
using SurveyApp.Application.Services.Repositories;

namespace SurveyApp.Application.Features.SurveyReports.Queries.GetSurveyReport
{
		public class GetSurveyReportQuery : IRequest<GetSurveyReportResponse>
		{
			public int SurveyId { get; set; }
	}
	
		public class GetSurveyReportQueryHandler
			: IRequestHandler<GetSurveyReportQuery, GetSurveyReportResponse>
		{
			private readonly IUserSurveyRepository _userSurveyRepository;

			public GetSurveyReportQueryHandler(IUserSurveyRepository userSurveyRepository)
			{
				_userSurveyRepository = userSurveyRepository;
			}

			public async Task<GetSurveyReportResponse> Handle(
				GetSurveyReportQuery request,
				CancellationToken cancellationToken)
			{
				var userSurveys = await _userSurveyRepository.GetListNoPaginationAsync(
					predicate: us => us.SurveyId == request.SurveyId,
					include: q => q.Include(us => us.User),
					enableTracking: false,
					cancellationToken: cancellationToken
				);

				return new GetSurveyReportResponse
				{
					SurveyId = request.SurveyId,
					Users = userSurveys.Select(us => new UserSurveyReportDto
					{
						UserId = us.UserId,
						FirstName = us.User!.FirstName,
						LastName = us.User.LastName,
						IsCompleted = us.IsCompleted
					}).ToList()
				};
			}
		}
	}




