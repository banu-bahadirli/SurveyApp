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
				//1.email ile kullanıcı kontrolü
				var user = await _userRepository.GetAsync(u => u.Email == command.Email);
				if(user == null)
				{
					return new LoginResponse
					{
						Success = false,
						Message = LoginMessages.UserNotFound
					};
				}

				//2.şifre doğrulama
				if (!HashingHelper.VerifyPasswordHash(command.Password, user.PasswordHash, user.PasswordSalt))
				{
					return new LoginResponse
					{
						Success = false,
						Message =LoginMessages.InvalidEmailPassword
					};
				}


				// 3. Kullanıcının claimlerini al
				var operationClaims = await _userOperationClaimRepository.GetOperationClaimsByUserIdAsync(user.Id);

				// 4. AccessToken oluştur
				var accessToken = _tokenHelper.CreateToken(user, operationClaims);

				// 5. Response map et
				var response = new LoginResponse
				{
					Id = user.Id,
					FirstName=user.FirstName,
					LastName=user.LastName,
					AccessToken = accessToken,
					Success = true,
					Message = LoginMessages.SuccessLogin
				};
				return response;




			}
		}
	}
}
