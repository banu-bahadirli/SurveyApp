using Microsoft.AspNetCore.Http;
using SurveyApp.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Persistance.Repositories
{
	public class UserSessionRepository : IUserSessionRepository
	{

			private readonly IHttpContextAccessor _httpContextAccessor;
			public UserSessionRepository(IHttpContextAccessor httpContextAccessor)
			{
				_httpContextAccessor = httpContextAccessor;
			}

			public string GetUserId()
			{
				return _httpContextAccessor.HttpContext?.User
					.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			}
		

	}
}
