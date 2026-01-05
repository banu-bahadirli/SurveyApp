using MediatR;
using SurveyApp.Application.Features.Auth.Login.Constants;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Security.Hashing;
using SurveyApp.Core.Security.JWT;


namespace SurveyApp.Application.Features.Auth.Login.Commands
{
	public class LoginCommand : IRequest<LoginResponse>
	{
		public string Email { get; set; }
		public string Password { get; set; }

		public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
		{
			private readonly IUserRepository _userRepository;
			private readonly IUserOperationClaimRepository _userOperationClaimRepository;
			private readonly ITokenHelper _tokenHelper;

			public LoginCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository,ITokenHelper tokenHelper)
			{
				_userRepository = userRepository;
				_userOperationClaimRepository = userOperationClaimRepository;
				_tokenHelper = tokenHelper;
			}

			public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
			{
				#region email ile kullanıcı kontrolü
				var user = await _userRepository.GetAsync(u => u.Email == command.Email);
				if(user == null)
				{
					return new LoginResponse
					{
						Success = false,
						Message = LoginMessages.UserNotFound
					};
				}
				#endregion

				#region 
				if (!HashingHelper.VerifyPasswordHash(command.Password, user.PasswordHash, user.PasswordSalt))
				{
					return new LoginResponse
					{
						Success = false,
						Message =LoginMessages.InvalidEmailPassword
					};
				}
				#endregion


				#region Kullanıcının claimlerini al
				var operationClaims = await _userOperationClaimRepository.GetOperationClaimsByUserIdAsync(user.Id);
				#endregion


				var accessToken = _tokenHelper.CreateToken(user, operationClaims);
				var response = new LoginResponse
				{
					Id = user.Id,
					FirstName=user.FirstName,
					LastName=user.LastName,
					AccessToken = accessToken,
					Roles = operationClaims.Select(p=>p.Name).ToList(),
					Success = true,
					Message = LoginMessages.SuccessLogin
				};
				return response;




			}
		}
	}
}
