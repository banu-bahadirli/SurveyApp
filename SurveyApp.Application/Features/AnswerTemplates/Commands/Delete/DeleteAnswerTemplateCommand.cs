using MediatR;
using SurveyApp.Application.Features.AnswerTemplates.Constants;
using SurveyApp.Application.Services.Repositories;

namespace SurveyApp.Application.Features.AnswerTemplates.Commands.Delete
{
	public class DeleteAnswerTemplateCommand : IRequest<DeletedAnswerTemplateResponse>
	{
		public int Id { get; set; }

		public class DeleteAnswerTemplateCommandHandler : IRequestHandler<DeleteAnswerTemplateCommand, DeletedAnswerTemplateResponse>
		{
			private readonly IAnswerTemplateRepository _answerTemplateRepository;

			public DeleteAnswerTemplateCommandHandler(IAnswerTemplateRepository answerTemplateRepository)
			{
				_answerTemplateRepository = answerTemplateRepository;
			}

			public async Task<DeletedAnswerTemplateResponse> Handle(DeleteAnswerTemplateCommand command, CancellationToken cancellationToken)
			{
				// Mevcut şablonu al
				var answerTemplate = await _answerTemplateRepository.GetAsync(
					c => c.Id == command.Id,
					cancellationToken: cancellationToken
				);

				if (answerTemplate == null)
					throw new Exception("Cevap şablonu bulunamadı"); // Daha sonra NotFoundException ekleyebilirsin

				// Şablonu sil
				await _answerTemplateRepository.DeleteAsync(answerTemplate,true);

				// Response dön
				return new DeletedAnswerTemplateResponse
				{
					Id = command.Id,
					Message = AnswerTemplateMessages.AnswerTemplateDeleted,
					Success = true
				};
			}
		}
	}
}
