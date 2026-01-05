namespace SurveyApp.Application.Validation;
using FluentValidation;
using SurveyApp.Application.Features.Surveys.Commands.Create;

public class CreateSurveyCommandValidator : AbstractValidator<CreateSurveyCommand>
{
	public CreateSurveyCommandValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty().WithMessage("Anket başlığı boş olamaz.")
			.MaximumLength(100).WithMessage("Anket başlığı 200 karakterden uzun olamaz.");

		RuleFor(x => x.StartDate)
			.LessThan(x => x.EndDate).WithMessage("Başlangıç tarihi, bitiş tarihinden önce olmalıdır.");

		RuleFor(x => x.EndDate)
			.GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi, başlangıç tarihinden sonra olmalıdır.");

	}
}
