using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.Create
{
	public class CreateSurveyCommandValidator : AbstractValidator<CreateSurveyCommand>
	{
		public CreateSurveyCommandValidator()
		{

			RuleFor(c=>c.Title).NotEmpty().MinimumLength(20);
			RuleFor(c => c.Description).NotEmpty().MinimumLength(50);
		}
	}
}
