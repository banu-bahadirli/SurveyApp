using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Features.AnswerTemplates.Constants;
using SurveyApp.Application.Features.AnswerTemplates.Dtos;
using SurveyApp.Application.Features.AnswerTemplates.Rules;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.AnswerTemplates.Commands.Update
{
	public class UpdateAnswerTemplateCommand : IRequest<UpdatedAnswerTemplateResponse>
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int OptionCount { get; set; }
		public List<AnswerOptionDto> Options { get; set; } = new();

		public class UpdateAnswerTemplateCommandHandler : IRequestHandler<UpdateAnswerTemplateCommand, UpdatedAnswerTemplateResponse>
		{
			private readonly IAnswerTemplateRepository _answerTemplateRepository;
			private readonly IMapper _mapper;
			private readonly AnswerTemplateBusinessRules _rules;

			public UpdateAnswerTemplateCommandHandler(
				IAnswerTemplateRepository answerTemplateRepository,
				IMapper mapper,
				AnswerTemplateBusinessRules rules)
			{
				_answerTemplateRepository = answerTemplateRepository;
				_mapper = mapper;
				_rules = rules;
			}

			public async Task<UpdatedAnswerTemplateResponse> Handle(UpdateAnswerTemplateCommand command, CancellationToken cancellationToken)
			{
				var answerTemplate = await _answerTemplateRepository.GetAsync(
					c => c.Id == command.Id,
					include: c => c.Include(x => x.Options),
					cancellationToken: cancellationToken
				);

				if (answerTemplate == null)
				{
					return new UpdatedAnswerTemplateResponse
					{
						Success = false,
						Message = AnswerTemplateMessages.AnswerTemplateNotFound
					};
				}

                #region Business Rules kontrolleri
				var optionCountMessage = await _rules.OptionCountMustBeBetween2And4(command.OptionCount);
				if (optionCountMessage != null)
				{
					return new UpdatedAnswerTemplateResponse
					{
						Success = false,
						Message = optionCountMessage
					};
				}

				var optionsMatchMessage = await _rules.OptionCountMustMatchOptions(command.OptionCount, command.Options.Count);
				if (optionsMatchMessage != null)
				{
					return new UpdatedAnswerTemplateResponse
					{
						Success = false,
						Message = optionsMatchMessage
					};
				}
				#endregion

				answerTemplate.Name = command.Name;
				answerTemplate.OptionCount = command.OptionCount;

				answerTemplate.Options.Clear();
				answerTemplate.Options = command.Options
					.Select(o => new AnswerOption
					{
						Text = o.Text,
						Order = o.Order
					}).ToList();

				var updatedTemplate = await _answerTemplateRepository.UpdateAsync(answerTemplate, cancellationToken);

				var response = _mapper.Map<UpdatedAnswerTemplateResponse>(updatedTemplate);
				response.Message = AnswerTemplateMessages.AnswerTemplateUpdated;
				response.Success = true;
				return response;
			}
		}
	}
}
