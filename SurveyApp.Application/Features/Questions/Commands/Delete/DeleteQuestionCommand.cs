using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.Questions.Constants;
using SurveyApp.Application.Services.Repositories;


namespace SurveyApp.Application.Features.Questions.Commands.Delete
{
	public class DeleteQuestionCommand : IRequest<DeletedQuestionResponse>
	{
		public int Id { get; set; }

		public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, DeletedQuestionResponse>
		{
			private readonly IQuestionRespository _questionRespository;
			private readonly IMapper _mapper;

			public DeleteQuestionCommandHandler(
				IQuestionRespository questionRespository,
			IMapper mapper)
			{
				_questionRespository = questionRespository;
				_mapper = mapper;
			}

			public async Task<DeletedQuestionResponse> Handle(DeleteQuestionCommand command, CancellationToken cancellationToken)
			{
				var question = await _questionRespository.GetAsync(c => c.Id == command.Id);
				await _questionRespository.DeleteAsync(question);

				return new DeletedQuestionResponse
				{
					Id = command.Id,
					Message = QuestionMessages.QuestionDeleted,
					Success =true
				};
			}
		}
	}
}
