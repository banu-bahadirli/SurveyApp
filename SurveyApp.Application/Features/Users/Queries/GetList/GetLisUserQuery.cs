using AutoMapper;
using MediatR;
using SurveyApp.Application.Services.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Users.Queries.GetList
{
	public class GetListUserQuery : IRequest<List<GetListUserResponse>>
	{
		public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, List<GetListUserResponse>>
		{
			private readonly IUserRepository _userRepository;
			private readonly IMapper _mapper;

			public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
			{
				_userRepository = userRepository;
				_mapper = mapper;
			}

			public async Task<List<GetListUserResponse>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
			{
				var users = await _userRepository.GetListNoPaginationAsync(c => c.Status == true);
				return _mapper.Map<List<GetListUserResponse>>(users);
			}
		}
	}
}
