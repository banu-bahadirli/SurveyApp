using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.Questions.Constants;
using SurveyApp.Application.Features.Questions.Rules;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;


namespace SurveyApp.Application.Features.Questions.Commands.Create;

public class CreateQuestionCommand : IRequest<CreatedQuestionResponse>
{
	public string Text { get; set; } = string.Empty;        // Soru metni
	public int AnswerTemplateId { get; set; }               // Önceden tanımlanmış şablon

	public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, CreatedQuestionResponse>
	{
		private readonly IQuestionRespository _questionRespository;
		private readonly QuestionBusinessRules _questionBusinessRules;
		private readonly IMapper _mapper;

		public CreateQuestionCommandHandler(
			IQuestionRespository questionRespository,
			IMapper mapper,
			QuestionBusinessRules questionBusinessRules)
		{
			_questionRespository = questionRespository;
			_mapper = mapper;
			_questionBusinessRules = questionBusinessRules;
		}

		public async Task<CreatedQuestionResponse> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
		{
			await _questionBusinessRules.AnswerTemplateMustExist(command.AnswerTemplateId);

			var question = _mapper.Map<Question>(command);
			var createdQuestion = await _questionRespository.AddAsync(question);

			var response = _mapper.Map<CreatedQuestionResponse>(createdQuestion);
			response.Message = QuestionMessages.QuestionCreated;

			return response;
		}
	}
}