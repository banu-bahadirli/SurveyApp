using FluentValidation;
using SurveyApp.Application.Features.AnswerTemplates.Commands.Create;

public class CreateAnswerTemplateCommandValidator : AbstractValidator<CreateAnswerTemplateCommand>
{
	public CreateAnswerTemplateCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Şablon adı boş olamaz")
			.MaximumLength(100);

		RuleFor(x => x.OptionCount)
			.InclusiveBetween(2, 4)
			.WithMessage("Şık sayısı 2 ile 4 arasında olmalı");

		RuleFor(x => x.Options)
			.NotEmpty().WithMessage("Şıklar boş olamaz")
			.Must((cmd, list) => list.Count == cmd.OptionCount)
			.WithMessage("Şık sayısı ile Options listesi eşleşmelidir");

		RuleForEach(x => x.Options)
			.Must(opt => !string.IsNullOrWhiteSpace(opt.Text))
			.WithMessage("Her şıkın metni boş olamaz");
	}
}
