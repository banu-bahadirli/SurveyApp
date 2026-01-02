using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core;

public static class SecurityServiceRegistration
{
	public static IServiceCollection AddSecurityServices(this IServiceCollection services)
	{
		services.AddScoped<ITokenHelper, JwtHelper>();
		return services;
	}
}