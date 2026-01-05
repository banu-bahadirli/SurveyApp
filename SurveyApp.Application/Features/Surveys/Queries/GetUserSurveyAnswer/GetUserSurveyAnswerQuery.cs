using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.Surveys.Queries.GetUserSurveyAnswers
{
	public class GetUserSurveyAnswersQuery : IRequest<List<GetUserSurveyAnswerResponse>>
	{
		public int SurveyId { get; set; }
		public int UserId { get; set; }

		public class GetUserSurveyAnswersQueryHandler : IRequestHandler<GetUserSurveyAnswersQuery, List<GetUserSurveyAnswerResponse>>
		{
			private readonly IUserSurveyAnswerRepository _userSurveyAnswerRepository;
			private readonly IMapper _mapper;

			public GetUserSurveyAnswersQueryHandler(
				IUserSurveyAnswerRepository userSurveyAnswerRepository,
				IMapper mapper)
			{
				_userSurveyAnswerRepository = userSurveyAnswerRepository;
				_mapper = mapper;
			}

			public async Task<List<GetUserSurveyAnswerResponse>> Handle(
				GetUserSurveyAnswersQuery request,
				CancellationToken cancellationToken)
			{
				var answers = await _userSurveyAnswerRepository.GetListNoPaginationAsync(
					predicate: ua => ua.UserSurvey!.SurveyId == request.SurveyId && ua.UserSurvey!.UserId == request.UserId,
					include: q => q.Include(ua => ua.Question)
								   .Include(ua => ua.SelectedOption),
					enableTracking: false,
					cancellationToken: cancellationToken
				);

				return _mapper.Map<List<GetUserSurveyAnswerResponse>>(answers);
			}
		}
	}
}
	
