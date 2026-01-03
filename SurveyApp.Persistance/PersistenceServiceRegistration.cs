using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Persistance.Context;
using SurveyApp.Persistance.Repositories;


namespace SurveyApp.Persistance
{
	public static class PersistenceServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			// DbContext
			services.AddDbContext<BaseDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});

			// Repositories
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
			services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
			services.AddScoped<IBrandRepository, BrandRepository>();
			services.AddScoped<ISurveyRepository, SurveyRepository>();
			services.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();
			services.AddScoped<IUserSurveyRepository, UserSurveyRepository>();
			services.AddScoped<IUserSurveyAnswerRepository, UserSurveyAnswerRepository>();
			services.AddScoped<IAnswerTemplateRepository, AnswerTemplateRepository>();
			services.AddScoped<IQuestionRespository, QuestionRespository>();
	

			services.AddScoped<IUserSessionRepository, UserSessionRepository>();

			// Business Rules
			//services.AddScoped<UserRegisterBusinessRules>();

			return services;
		}
	}
}
