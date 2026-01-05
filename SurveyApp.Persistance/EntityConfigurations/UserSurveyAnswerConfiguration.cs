using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Persistence.EntityConfigurations
{
	public class UserSurveyAnswerConfiguration : IEntityTypeConfiguration<UserSurveyAnswer>
	{
		public void Configure(EntityTypeBuilder<UserSurveyAnswer> builder)
		{
			builder.ToTable("UserSurveyAnswers")
				   .HasKey(a => a.Id); 

			builder.Property(a => a.Id).IsRequired();
			builder.Property(a => a.UserSurveyId).IsRequired();
			builder.Property(a => a.QuestionId).IsRequired();
			builder.Property(a => a.SelectedOptionId).IsRequired();

			// İlişkiler
			builder.HasOne(a => a.UserSurvey)
				   .WithMany(us => us.Answers)
				   .HasForeignKey(a => a.UserSurveyId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(a => a.Question)
				   .WithMany()
				   .HasForeignKey(a => a.QuestionId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(a => a.SelectedOption)
				   .WithMany()
				   .HasForeignKey(a => a.SelectedOptionId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
