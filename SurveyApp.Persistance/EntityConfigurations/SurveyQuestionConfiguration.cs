using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Persistence.EntityConfigurations
{
	public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
	{
		public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
		{
			builder.ToTable("SurveyQuestions")
				   .HasKey(sq => new { sq.SurveyId, sq.QuestionId }); // composite key

			// İlişkiler
			builder.HasOne(sq => sq.Survey)
				   .WithMany() 
				   .HasForeignKey(sq => sq.SurveyId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(sq => sq.Question)
				   .WithMany()
				   .HasForeignKey(sq => sq.QuestionId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
