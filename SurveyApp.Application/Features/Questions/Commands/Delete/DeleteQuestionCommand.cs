using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.Questions.Constants;
using SurveyApp.Application.Features.Questions.Rules;
using SurveyApp.Application.Services.Repositories;


namespace SurveyApp.Application.Features.Questions.Commands.Delete
{
	public class DeleteQuestionCommand : IRequest<DeletedQuestionResponse>
	{
		public int Id { get; set; }

		public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, DeletedQuestionResponse>
		{
			private readonly IQuestionRepository _questionRespository;
			private readonly QuestionBusinessRules _questionBusinessRules;
			private readonly IMapper _mapper;

			public DeleteQuestionCommandHandler(
				IQuestionRepository questionRespository, QuestionBusinessRules questionBusinessRules,IMapper mapper)
			{
				_questionRespository = questionRespository;
				_mapper = mapper;
				_questionBusinessRules = questionBusinessRules;
			}

			public async Task<DeletedQuestionResponse> Handle(DeleteQuestionCommand command, CancellationToken cancellationToken)
			{
				#region Soru herhangi bir ankette kullanılmış mı?
				var ruleMessage = await _questionBusinessRules.QuestionCannotBeDeletedIfUsedInSurvey(command.Id);
				if(ruleMessage!=null)
				{
					return new DeletedQuestionResponse
					{
						Id = command.Id,
						Message = QuestionMessages.QuestionUsedInSurveyCannotBeDeleted,
						Success = false					
					};

				}				
				#endregion

				var question = await _questionRespository.GetAsync(c => c.Id == command.Id);
				await _questionRespository.DeleteAsync(question,true);

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
