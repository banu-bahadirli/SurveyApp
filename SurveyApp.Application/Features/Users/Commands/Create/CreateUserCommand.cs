using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.Surveys.Commands.Create;
using SurveyApp.Application.Features.Users.Constants;
using SurveyApp.Application.Features.Users.Rules;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Security.Constants;
using SurveyApp.Core.Security.Entities;
using SurveyApp.Core.Security.Hashing;

namespace SurveyApp.Application.Features.Users.Commands.Create
{
	public class CreateUserCommand : IRequest<CreatedUserResponse>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserResponse>
		{
			private readonly IUserRepository _userRepository;
			private readonly UserBusinessRules _businessRules;
			private readonly IMapper _mapper;
			private readonly IUserOperationClaimRepository _userOperationClaimRepository;
			private readonly IOperationClaimRepository _operationClaimRepository;

			public CreateUserCommandHandler(
				IUserRepository userRepository,
				IMapper mapper,
				UserBusinessRules businessRules,
				IUserOperationClaimRepository userOperationClaimRepository,
				IOperationClaimRepository operationClaimRepository)
			{
				_userRepository = userRepository;
				_mapper = mapper;
				_businessRules = businessRules;
				_userOperationClaimRepository = userOperationClaimRepository;
				_operationClaimRepository = operationClaimRepository;
			}

			public async Task<CreatedUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
			{
				#region Email uniqueness kontrolü
				var bussinessMessage = await _businessRules.UserEmailCannotBeDuplicatedWhenInserted(command.Email);
				if (bussinessMessage != null)
				{
					return new CreatedUserResponse
					{
						Success = false,
						Message = bussinessMessage
					};
				}
				#endregion

				#region  Password hash oluştur

				HashingHelper.CreatePasswordHash(command.Password, out byte[] passwordHash, out byte[] passwordSalt);

				#endregion

				var user = _mapper.Map<User>(command);
				user.PasswordHash = passwordHash;
				user.PasswordSalt = passwordSalt;

				var createdUser = await _userRepository.AddAsync(user);

				// Default "User" rolünü DB'de kontrol et, yoksa ekle
				var userClaim = await _operationClaimRepository.GetAsync(c => c.Name == GeneralOperationClaims.User);
				if (userClaim == null)
				{
					userClaim = new OperationClaim
					{
						Name = GeneralOperationClaims.User,
						CreatedDate = DateTime.UtcNow
					};
					userClaim = await _operationClaimRepository.AddAsync(userClaim);
				}

				// Kullanıcıya User rolünü bağla
				await _userOperationClaimRepository.AddAsync(new UserOperationClaim(createdUser.Id, userClaim.Id));

				// Response oluştur
				var response = _mapper.Map<CreatedUserResponse>(createdUser);
				response.Message = UserMessages.UserCreated;
				response.Success = true;

				return response;
			}
		}
	}
}
