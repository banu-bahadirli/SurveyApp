using AutoMapper;
using MediatR;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<GetByIdUserResponse>
{
	//public int Id { get; set; }

	public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IUserSessionRepository _userSessionRepository;

		public GetByIdUserQueryHandler(IMapper mapper, IUserRepository userRepository, IUserSessionRepository userSessionRepository)
		{
			_mapper = mapper;
			_userRepository= userRepository;
			_userSessionRepository= userSessionRepository;
		}

		public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
		{
			var userId = _userSessionRepository.GetUserId();

			//user is yi kontol et
			var user = await _userRepository.GetAsync(predicate: b => b.Id == int.Parse(userId), cancellationToken: cancellationToken);

			GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);

			return response;
		}
	}


}