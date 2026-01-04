using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.AnswerTemplates.Commands.Delete;
using SurveyApp.Application.Features.Questions.Constants;
using SurveyApp.Application.Features.Questions.Rules;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;


namespace SurveyApp.Application.Features.Questions.Commands.Create;

public class CreateQuestionCommand : IRequest<CreatedQuestionResponse>
{
	public string Text { get; set; } = string.Empty;      
	public int AnswerTemplateId { get; set; }             

	public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, CreatedQuestionResponse>
	{
		private readonly IQuestionRepository _questionRespository;
		private readonly QuestionBusinessRules _questionBusinessRules;
		private readonly IMapper _mapper;

		public CreateQuestionCommandHandler(
			IQuestionRepository questionRespository,
			IMapper mapper,
			QuestionBusinessRules questionBusinessRules)
		{
			_questionRespository = questionRespository;
			_mapper = mapper;
			_questionBusinessRules = questionBusinessRules;
		}

		public async Task<CreatedQuestionResponse> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
		{
			var businessMessage =  await _questionBusinessRules.AnswerTemplateMustExist(command.AnswerTemplateId);
			if (businessMessage != null)
			{
				return new CreatedQuestionResponse
				{
					Success = false,
					Message = businessMessage
				};
			}

			var question = _mapper.Map<Question>(command);
			var createdQuestion = await _questionRespository.AddAsync(question);

			var response = _mapper.Map<CreatedQuestionResponse>(createdQuestion);
			response.Message = QuestionMessages.QuestionCreated;

			return response;
		}
	}
}