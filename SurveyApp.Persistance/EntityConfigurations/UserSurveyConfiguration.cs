using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Persistence.EntityConfigurations
{
	public class UserSurveyConfiguration : IEntityTypeConfiguration<UserSurvey>
	{
		public void Configure(EntityTypeBuilder<UserSurvey> builder)
		{
			builder.ToTable("UserSurveys")
				   .HasKey(us => us.Id);

			builder.Property(us => us.Id).IsRequired();
			builder.Property(us => us.UserId).IsRequired();
			builder.Property(us => us.SurveyId).IsRequired();
			builder.Property(us => us.IsCompleted).IsRequired();
			builder.Property(us => us.CreatedDate).IsRequired();

			
			builder.HasOne(us => us.User)
				   .WithMany() 
				   .HasForeignKey(us => us.UserId)
				   .OnDelete(DeleteBehavior.Restrict);

			
			builder.HasOne(us => us.Survey)
				   .WithMany(s => s.UserSurveys) 
				   .HasForeignKey(us => us.SurveyId)
				   .OnDelete(DeleteBehavior.Cascade);

			
			builder.HasMany(us => us.Answers)
				   .WithOne(a => a.UserSurvey)
				   .HasForeignKey(a => a.UserSurveyId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
