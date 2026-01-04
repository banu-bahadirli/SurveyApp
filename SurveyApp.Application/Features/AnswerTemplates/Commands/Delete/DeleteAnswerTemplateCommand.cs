using MediatR;
using SurveyApp.Application.Features.AnswerTemplates.Constants;
using SurveyApp.Application.Features.AnswerTemplates.Rules;
using SurveyApp.Application.Services.Repositories;

namespace SurveyApp.Application.Features.AnswerTemplates.Commands.Delete
{
	public class DeleteAnswerTemplateCommand : IRequest<DeletedAnswerTemplateResponse>
	{
		public int Id { get; set; }

		public class DeleteAnswerTemplateCommandHandler
			: IRequestHandler<DeleteAnswerTemplateCommand, DeletedAnswerTemplateResponse>
		{
			private readonly IAnswerTemplateRepository _answerTemplateRepository;
			private readonly AnswerTemplateBusinessRules _businessRules;

			public DeleteAnswerTemplateCommandHandler(
				IAnswerTemplateRepository answerTemplateRepository,
				AnswerTemplateBusinessRules businessRules)
			{
				_answerTemplateRepository = answerTemplateRepository;
				_businessRules = businessRules;
			}

			public async Task<DeletedAnswerTemplateResponse> Handle(
				DeleteAnswerTemplateCommand command,
				CancellationToken cancellationToken)
			{
				var answerTemplate = await _answerTemplateRepository.GetAsync(
					at => at.Id == command.Id,
					cancellationToken: cancellationToken
				);

				if (answerTemplate == null)
				{
					return new DeletedAnswerTemplateResponse
					{
						Id = command.Id,
						Success = false,
						Message = AnswerTemplateMessages.AnswerTemplateNotFound
					};
				}

                #region Business Rule kontrolü
				var businessMessage = await _businessRules.CanAnswerTemplateBeDeleted(command.Id);

				if (businessMessage != null)
				{
					return new DeletedAnswerTemplateResponse
					{
						Id = command.Id,
						Success = false,
						Message = businessMessage
					};
				}
				#endregion

				await _answerTemplateRepository.DeleteAsync(answerTemplate, true);

				return new DeletedAnswerTemplateResponse
				{
					Id = command.Id,
					Success = true,
					Message = AnswerTemplateMessages.AnswerTemplateDeleted
				};
			}
		}
	}
}
