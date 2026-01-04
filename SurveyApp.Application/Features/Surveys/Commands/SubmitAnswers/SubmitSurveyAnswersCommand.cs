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
		public int UserSurveyId { get; set; }
		public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
	}

	public class SubmitSurveyAnswersCommandHandler : IRequestHandler<SubmitSurveyAnswersCommand, SubmitSurveyAnswersResponse>
	{
		private readonly IUserSurveyRepository _userSurveyRepository;
		private readonly IUserSurveyAnswerRepository _userSurveyAnswerRepository;
		private readonly ITransactionalRepository _transactionalRepository;

		public SubmitSurveyAnswersCommandHandler(
			IUserSurveyRepository userSurveyRepository,
			IUserSurveyAnswerRepository userSurveyAnswerRepository,
			ITransactionalRepository transactionalRepository)
		{
			_userSurveyRepository = userSurveyRepository;
			_userSurveyAnswerRepository = userSurveyAnswerRepository;
			_transactionalRepository = transactionalRepository;
		}

		public async Task<SubmitSurveyAnswersResponse> Handle(
			SubmitSurveyAnswersCommand request,
			CancellationToken cancellationToken)
		{
			await _transactionalRepository.BeginTransactionAsync();

			try
			{
				var userSurvey = await _userSurveyRepository.GetAsync(
					us => us.Id == request.UserSurveyId && !us.IsCompleted,
					include: q => q.Include(us => us.Survey),
					enableTracking: true,
					cancellationToken: cancellationToken
				);

				if (userSurvey == null)
					throw new Exception("Bu anket daha önce tamamlanmış veya bulunamadı.");

				foreach (var answer in request.Answers)
				{
					var userAnswer = new UserSurveyAnswer
					{
						UserSurveyId = request.UserSurveyId,
						QuestionId = answer.QuestionId,
						SelectedOptionId = answer.SelectedOptionId
					};

					await _userSurveyAnswerRepository.AddAsync(userAnswer, cancellationToken);
				}

				userSurvey.IsCompleted = true;
				await _userSurveyRepository.UpdateAsync(userSurvey, cancellationToken);

				await _transactionalRepository.CommitTransactionAsync();

				return new SubmitSurveyAnswersResponse
				{
					UserSurveyId = userSurvey.Id,
					Success = true,
					Message = SurveyMessages.SurveyAnswerCompleted
				};
			}
			catch
			{
				await _transactionalRepository.RollbackTransactionAsync();
				throw;
			}
		}
	}
}
