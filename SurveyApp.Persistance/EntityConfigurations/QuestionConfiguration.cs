using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Persistence.EntityConfigurations
{
	public class QuestionConfiguration : IEntityTypeConfiguration<Question>
	{
		public void Configure(EntityTypeBuilder<Question> builder)
		{
			builder.ToTable("Questions");
			builder.HasKey(q => q.Id);
			builder.Property(q => q.Id).IsRequired();
			builder.Property(q => q.Text)
				   .IsRequired()
				   .HasMaxLength(500);

			builder.Property(q => q.AnswerTemplateId)
				   .IsRequired();

			builder.Property(q => q.CreatedDate)
				   .IsRequired();

			builder.Property(q => q.UpdatedDate);

			builder.HasOne(q => q.AnswerTemplate)
				   .WithMany()
				   .HasForeignKey(q => q.AnswerTemplateId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(q => q.SurveyQuestions)
				   .WithOne(sq => sq.Question)
				   .HasForeignKey(sq => sq.QuestionId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasQueryFilter(q => !q.DeletedDate.HasValue);


		}
	}
}
