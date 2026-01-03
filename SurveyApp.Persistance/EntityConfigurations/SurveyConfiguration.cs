using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace Persistance.EntityConfigurations
{
	public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
	{
		public void Configure(EntityTypeBuilder<Survey> builder)
		{
			builder.ToTable("Surveys").HasKey(s => s.Id);

			builder.Property(s => s.Id).IsRequired();

			// Sadece bu alanları zorunlu yapıyoruz
			builder.Property(s => s.Title)
				   .IsRequired();

			builder.Property(s => s.StartDate)
				   .IsRequired();

			builder.Property(s => s.EndDate)
				   .IsRequired();

			// Diğer alanlar nullable olarak kalabilir
			builder.Property(s => s.CreatedDate).IsRequired();
			builder.Property(s => s.UpdatedDate);
			builder.Property(s => s.DeletedDate);

			// Soft delete için query filter
			builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
		}
	}
}
