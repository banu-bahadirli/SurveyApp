using SurveyApp.Application.Features.Surveys.Constants;
using SurveyApp.Application.Features.Users.Constants;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Users.Rules
{
	public class UserBusinessRules
	{

		private readonly IUserRepository _userRepository;

		public UserBusinessRules(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		#region Aynı Email kayıtlı mı
		public async Task<string?> UserEmailCannotBeDuplicatedWhenInserted(string email)
		{
			var result = await _userRepository.AnyAsync(c => c.Email == email);

			if (result)
			{
				return UserMessages.UserEmailAlreadyExists;

			}
			return null;
		}
		#endregion
	}
}
