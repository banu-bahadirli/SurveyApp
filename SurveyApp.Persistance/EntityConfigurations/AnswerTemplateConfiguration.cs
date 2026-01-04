using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Persistence.EntityConfigurations
{
	public class AnswerTemplateConfiguration : IEntityTypeConfiguration<AnswerTemplate>
	{
		public void Configure(EntityTypeBuilder<AnswerTemplate> builder)
		{
			builder.ToTable("AnswerTemplates");

			builder.HasKey(at => at.Id);

			builder.Property(at => at.Id)
				   .IsRequired();

			builder.Property(at => at.Name)
				   .IsRequired()
				   .HasMaxLength(200);

			builder.Property(at => at.OptionCount)
				   .IsRequired();

			builder.Property(at => at.CreatedDate)
				   .IsRequired();

			builder.Property(at => at.UpdatedDate);
			builder.Property(at => at.DeletedDate);

			builder.HasMany(at => at.Options)
				   .WithOne(o => o.AnswerTemplate)
				   .HasForeignKey(o => o.AnswerTemplateId)
				   .OnDelete(DeleteBehavior.Cascade);
			builder.HasQueryFilter(at => !at.DeletedDate.HasValue);
		}
	}
}
