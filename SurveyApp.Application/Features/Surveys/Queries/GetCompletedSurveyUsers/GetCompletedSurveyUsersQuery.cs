using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Services.Repositories;

namespace SurveyApp.Application.Features.Surveys.Queries.GetCompletedSurveyUser;

public class GetCompletedSurveyUserQuery : IRequest<List<GetCompletedSurveyUserResponse>>
{
	public int SurveyId { get; set; }
}

public class GetCompletedSurveyUserQueryHandler
	: IRequestHandler<GetCompletedSurveyUserQuery, List<GetCompletedSurveyUserResponse>>
{
	private readonly IUserSurveyRepository _userSurveyRepository;
	private readonly IMapper _mapper;

	public GetCompletedSurveyUserQueryHandler(
		IUserSurveyRepository userSurveyRepository,
		IMapper mapper)
	{
		_userSurveyRepository = userSurveyRepository;
		_mapper = mapper;
	}

	public async Task<List<GetCompletedSurveyUserResponse>> Handle(
		GetCompletedSurveyUserQuery request,
		CancellationToken cancellationToken)
	{
		var completedUsers = await _userSurveyRepository.GetListNoPaginationAsync(
			predicate: us => us.SurveyId == request.SurveyId && us.IsCompleted,
			include: q => q.Include(us => us.User),
			enableTracking: false,
			cancellationToken: cancellationToken
		);
		return _mapper.Map<List<GetCompletedSurveyUserResponse>>(completedUsers);
	}
}
