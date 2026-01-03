using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Persistence.EntityConfigurations
{
	public class SurveyAnswerConfiguration : IEntityTypeConfiguration<SurveyAnswer>
	{
		public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
		{
			builder.ToTable("SurveyAnswers").HasKey(sa => sa.Id);

			builder.Property(sa => sa.Id).IsRequired();
			builder.Property(sa => sa.UserId).IsRequired();
			builder.Property(sa => sa.SurveyId).IsRequired();
			builder.Property(sa => sa.QuestionId).IsRequired();
			builder.Property(sa => sa.AnswerOptionId).IsRequired();

			// ilişkiler
			builder.HasOne(sa => sa.User)
				   .WithMany() 
				   .HasForeignKey(sa => sa.UserId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(sa => sa.Survey)
				   .WithMany()
				   .HasForeignKey(sa => sa.SurveyId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(sa => sa.Question)
				   .WithMany()
				   .HasForeignKey(sa => sa.QuestionId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(sa => sa.AnswerOption)
				   .WithMany()
				   .HasForeignKey(sa => sa.AnswerOptionId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
