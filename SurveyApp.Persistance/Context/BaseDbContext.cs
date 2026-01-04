using Microsoft.EntityFrameworkCore;
using SurveyApp.Core.Security.Entities;
using SurveyApp.Domain.Entities;
using System.Reflection;

namespace SurveyApp.Persistance.Context
{
	public class BaseDbContext : DbContext
	{
		public DbSet<OperationClaim> OperationClaims { get; set; }
		public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<AnswerTemplate> AnswerTemplates { get; set; }
		public DbSet<AnswerOption> AnswerOptions { get; set; }
		public DbSet<Survey> Surveys { get; set; } = null!;
		public DbSet<Question> Questions { get; set; } = null!;
		public DbSet<SurveyQuestion> SurveyQuestions { get; set; } = null!;
		public DbSet<UserSurvey> UserSurveys { get; set; } = null!;
		public DbSet<UserSurveyAnswer> UserSurveyAnswers { get; set; } = null!;

		public BaseDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Tüm configuration dosyalarını otomatik uygula
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		}
	}
}
