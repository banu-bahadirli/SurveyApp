using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.AnswerTemplates.Commands.Update;
using SurveyApp.Application.Features.AnswerTemplates.Constants;
using SurveyApp.Application.Features.Questions.Constants;
using SurveyApp.Application.Features.Surveys.Commands.Update;
using SurveyApp.Application.Services.Repositories;

namespace SurveyApp.Application.Features.Questions.Commands.Update
{
	public class UpdateQuestionCommand : IRequest<UpdatedQuestionResponse>
	{
		public int Id { get; set; } 
		public string Text { get; set; } = string.Empty;      
		public int AnswerTemplateId { get; set; }
	}

	public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, UpdatedQuestionResponse>
	{
		private readonly IQuestionRepository _questionRespository;
		private readonly IMapper _mapper;


		public UpdateQuestionCommandHandler(
			IQuestionRepository questionRespository,
			IMapper mapper)
		{
			_questionRespository = questionRespository;
			_mapper = mapper;
		}

		public async Task<UpdatedQuestionResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
		{

			var question = await _questionRespository.GetAsync(
				predicate: b => b.Id == request.Id,
				cancellationToken: cancellationToken
			);

			if (question == null)
				return new UpdatedQuestionResponse
				{
					Success = false,
					Message = QuestionMessages.QuestionNotFound
				};

			_mapper.Map(request, question);
			await _questionRespository.UpdateAsync(question, cancellationToken);
			var response = _mapper.Map<UpdatedQuestionResponse>(question);
			response.Success = true;
			response.Message = QuestionMessages.OuestioneUpdated;
			return response;
		}
	}
}
