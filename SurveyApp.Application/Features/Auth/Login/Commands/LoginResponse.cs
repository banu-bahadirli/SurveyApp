using SurveyApp.Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Auth.Login.Commands
{
	public class LoginResponse
	{
		public int Id { get; set; }	
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public AccessToken AccessToken { get; set; } = default!;
		public string Message { get; set; } = string.Empty;
		public bool Success { get; set; }
	}
}
