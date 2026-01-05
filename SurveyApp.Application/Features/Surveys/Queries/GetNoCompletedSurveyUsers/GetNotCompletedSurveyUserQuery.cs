using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Services.Repositories;

namespace SurveyApp.Application.Features.Surveys.Queries.GetNotCompletedSurveyUser;

public class GetNotCompletedSurveyUserQuery : IRequest<List<GetNotCompletedSurveyUserResponse>>
{
	public int SurveyId { get; set; }
}

public class GetNotCompletedSurveyUserQueryHandler
	: IRequestHandler<GetNotCompletedSurveyUserQuery, List<GetNotCompletedSurveyUserResponse>>
{
	private readonly IUserSurveyRepository _userSurveyRepository;
	private readonly IMapper _mapper;

	public GetNotCompletedSurveyUserQueryHandler(
		IUserSurveyRepository userSurveyRepository,
		IMapper mapper)
	{
		_userSurveyRepository = userSurveyRepository;
		_mapper = mapper;
	}

	public async Task<List<GetNotCompletedSurveyUserResponse>> Handle(
		GetNotCompletedSurveyUserQuery request,
		CancellationToken cancellationToken)
	{
		// Anketi doldurmayan kullanıcıları getiriyoruz
		var notCompletedUsers = await _userSurveyRepository.GetListNoPaginationAsync(
			predicate: us => us.SurveyId == request.SurveyId && !us.IsCompleted,
			include: q => q.Include(us => us.User),
			enableTracking: false,
			cancellationToken: cancellationToken
		);

		return _mapper.Map<List<GetNotCompletedSurveyUserResponse>>(notCompletedUsers);
	}
}
