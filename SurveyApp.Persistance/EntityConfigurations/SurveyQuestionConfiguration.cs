using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Persistence.EntityConfigurations
{
	public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
	{
		public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
		{
			builder.ToTable("SurveyQuestions");

			// PK
			builder.HasKey(sq => sq.Id);

			builder.Property(sq => sq.Id).IsRequired();
			builder.Property(sq => sq.SurveyId).IsRequired();
			builder.Property(sq => sq.QuestionId).IsRequired();
			builder.Property(sq => sq.CreatedDate).IsRequired();

			// Survey ile ilişki
			builder.HasOne(sq => sq.Survey)
				   .WithMany(s => s.SurveyQuestions) // Survey entity’de collection var
				   .HasForeignKey(sq => sq.SurveyId)
				   .OnDelete(DeleteBehavior.Cascade);

			// Question ile ilişki
			builder.HasOne(sq => sq.Question)
				   .WithMany() // Question entity’de collection yok
				   .HasForeignKey(sq => sq.QuestionId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
