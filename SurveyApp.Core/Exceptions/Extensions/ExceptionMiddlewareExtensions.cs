using Microsoft.AspNetCore.Builder;
using SurveyApp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.Exceptions.Extensions;

public static class ExceptionMiddlewareExtensions
{
	public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
		=> app.UseMiddleware<ExceptionMiddleware>();
}