

using FluentValidation;
using global::SurveyApp.Application.Services.Repositories;
using SurveyApp.Application.Services.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Questions.Commands.Create
{
	public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
	{
		private readonly IAnswerTemplateRepository _answerTemplateRepository;

		public CreateQuestionCommandValidator(IAnswerTemplateRepository answerTemplateRepository)
		{
			_answerTemplateRepository = answerTemplateRepository;

			RuleFor(x => x.Text)
				.NotEmpty().WithMessage("Soru metni boş olamaz")
				.MaximumLength(500);

			RuleFor(x => x.AnswerTemplateId)
				.NotEmpty().WithMessage("Cevap tipi (şablon) seçmelisiniz")
				.MustAsync(AnswerTemplateExists).WithMessage("Seçilen cevap şablonu bulunamadı");
		}

		private async Task<bool> AnswerTemplateExists(int id, CancellationToken cancellationToken)
		{
			return await _answerTemplateRepository.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
		}
	}
}
