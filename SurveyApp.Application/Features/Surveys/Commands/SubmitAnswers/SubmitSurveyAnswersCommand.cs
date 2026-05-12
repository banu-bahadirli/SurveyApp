using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Features.Surveys.Constants;
using SurveyApp.Application.Features.Surveys.Dtos;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Core.Persistance.Repositories;


namespace SurveyApp.Application.Features.Surveys.Commands.SubmitAnswers
{
	public class SubmitSurveyAnswersCommand : IRequest<SubmitSurveyAnswersResponse>
	{
		public int SurveyId { get; set; }
		public int UserId { get; set; }	
		public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
	}

	public class SubmitSurveyAnswersCommandHandler : IRequestHandler<SubmitSurveyAnswersCommand, SubmitSurveyAnswersResponse>
	{
		private readonly IUserSurveyRepository _userSurveyRepository;
		private readonly IUserSurveyAnswerRepository _userSurveyAnswerRepository;


		public SubmitSurveyAnswersCommandHandler(
			IUserSurveyRepository userSurveyRepository,
			IUserSurveyAnswerRepository userSurveyAnswerRepository)
		{
			_userSurveyRepository = userSurveyRepository;
			_userSurveyAnswerRepository = userSurveyAnswerRepository;
		}

		public async Task<SubmitSurveyAnswersResponse> Handle(
			SubmitSurveyAnswersCommand request,
			CancellationToken cancellationToken)
		{
			//await _transactionalRepository.BeginTransactionAsync();

			//try
			//{
			//	var userSurvey = await _userSurveyRepository.GetAsync(
			//		us => us.SurveyId == request.SurveyId && us.UserId == request.UserId &&
			//		!us.IsCompleted,
			//		include: q => q.Include(us => us.Survey),
			//		enableTracking: true,
			//		cancellationToken: cancellationToken
			//	);

			//	if (userSurvey == null)
			//	{
			//		return new SubmitSurveyAnswersResponse
			//		{
			//			Success = false,
			//			Message = SurveyMessages.SurveyAlreadyFilled
			//		};
			//	}

			//	foreach (var answer in request.Answers)
			//	{
			//		var userAnswer = new UserSurveyAnswer
			//		{
			//			UserSurveyId = userSurvey.Id,
			//			QuestionId = answer.QuestionId,
			//			SelectedOptionId = answer.SelectedOptionId
			//		};

			//		await _userSurveyAnswerRepository.AddAsync(userAnswer, cancellationToken);
			//	}

			//	userSurvey.IsCompleted = true;
			//	await _userSurveyRepository.UpdateAsync(userSurvey, cancellationToken);

			//	await _transactionalRepository.CommitTransactionAsync();

			//	return new SubmitSurveyAnswersResponse
			//	{
			//		UserSurveyId = userSurvey.Id,
			//		Success = true,
			//		Message = SurveyMessages.SurveyAnswerCompleted
			//	};
			//}
			//catch
			//{
			//	await _transactionalRepository.RollbackTransactionAsync();
			//	throw;
			//}
			return null;
		}
	}
}
