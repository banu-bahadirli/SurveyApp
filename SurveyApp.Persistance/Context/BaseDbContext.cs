
using Microsoft.EntityFrameworkCore;
using SurveyApp.Core.Security.Entities;
using SurveyApp.Domain.Entities;
using System.Collections.Generic;
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

		public BaseDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			// SurveyQuestion composite key
			modelBuilder.Entity<SurveyQuestion>()
				.HasKey(sq => new { sq.SurveyId, sq.QuestionId });

			modelBuilder.Entity<SurveyQuestion>()
				.HasOne(sq => sq.Survey)
				.WithMany(s => s.SurveyQuestions)
				.HasForeignKey(sq => sq.SurveyId);

			modelBuilder.Entity<SurveyQuestion>()
				.HasOne(sq => sq.Question)
				.WithMany()
				.HasForeignKey(sq => sq.QuestionId);

			// AnswerOption -> AnswerTemplate ilişkisi
			modelBuilder.Entity<AnswerOption>()
				.HasOne(o => o.AnswerTemplate)
				.WithMany(t => t.Options)
				.HasForeignKey(o => o.AnswerTemplateId)
				.OnDelete(DeleteBehavior.Cascade);

			// UserSurvey composite key
			modelBuilder.Entity<UserSurvey>()
				.HasKey(us => new { us.UserId, us.SurveyId });

			// UserSurvey -> Survey ilişkisi
			modelBuilder.Entity<UserSurvey>()
				.HasOne(us => us.Survey)
				.WithMany(s => s.UserSurveys)
				.HasForeignKey(us => us.SurveyId);
		}

	}
}
