using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Application.Features.AnswerTemplates.Rules;
using SurveyApp.Application.Features.Questions.Rules;
using SurveyApp.Application.Features.Surveys.Rules;
using SurveyApp.Application.Features.Users.Rules;
using System.Reflection;


namespace SurveyApp.Application;
public static class ApplicationServiceRegistration
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{

		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddMediatR(configuration =>
		{
			configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

		});
		services.AddScoped<SurveyBusinessRules>();
		services.AddScoped<UserBusinessRules>();
		services.AddScoped<AnswerTemplateBusinessRules>();
		services.AddScoped<QuestionBusinessRules>();

		return services;
	}
}