using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Features.Surveys.Constants;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.Update
{
	public class UpdateSurveyCommand : IRequest<UpdatedSurveyResponse>
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string? Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsActive { get; set; }
		public List<int> UserIds { get; set; } = new();
		public List<int> QuestionIds { get; set; } = new(); 
	}

	public class UpdateSurveyCommandHandler
		: IRequestHandler<UpdateSurveyCommand, UpdatedSurveyResponse>
	{
		private readonly ISurveyRepository _surveyRepository;
		private readonly IUserSurveyRepository _userSurveyRepository;
		private readonly ISurveyQuestionRepository _surveyQuestionRepository;
		private readonly IMapper _mapper;

		public UpdateSurveyCommandHandler(
			ISurveyRepository surveyRepository,
			IUserSurveyRepository userSurveyRepository,
			ISurveyQuestionRepository surveyQuestionRepository,
			IMapper mapper)
		{
			_surveyRepository = surveyRepository;
			_userSurveyRepository = userSurveyRepository;
			_surveyQuestionRepository = surveyQuestionRepository;
			_mapper = mapper;
		}

		public async Task<UpdatedSurveyResponse> Handle(
			UpdateSurveyCommand request,
			CancellationToken cancellationToken)
		{
			var survey = await _surveyRepository.GetAsync(
				s => s.Id == request.Id,
				include: q => q
					.Include(s => s.UserSurveys)
					.Include(s => s.SurveyQuestions),
				enableTracking: true,
				cancellationToken: cancellationToken
			);

			if (survey == null)
			{
				return new UpdatedSurveyResponse
				{
					Success = false,
					Message = SurveyMessages.SurveyNotFound
				};
			}
			
			_mapper.Map(request, survey);

			var oldUserSurveys = survey.UserSurveys.ToList();
			if (oldUserSurveys.Any())
			{
				foreach (var us in oldUserSurveys)
					await _userSurveyRepository.DeleteAsync(us, true);
			}

			foreach (var userId in request.UserIds)
			{
				var userSurvey = new UserSurvey
				{
					SurveyId = survey.Id,
					UserId = userId,
					IsCompleted = false,
					CreatedDate = DateTime.UtcNow
				};
				await _userSurveyRepository.AddAsync(userSurvey);
			}

			var oldSurveyQuestions = survey.SurveyQuestions.ToList();
			if (oldSurveyQuestions.Any())
			{
				foreach (var sq in oldSurveyQuestions)
					await _surveyQuestionRepository.DeleteAsync(sq, true);
			}

			foreach (var questionId in request.QuestionIds)
			{
				var surveyQuestion = new SurveyQuestion
				{
					SurveyId = survey.Id,
					QuestionId = questionId,
					CreatedDate = DateTime.UtcNow
				};
				await _surveyQuestionRepository.AddAsync(surveyQuestion);
			}

			await _surveyRepository.UpdateAsync(survey, cancellationToken);

			var response = _mapper.Map<UpdatedSurveyResponse>(survey);
			response.Success = true;
			response.Message = SurveyMessages.SurveyUpdated;
			return response;
		}
	}
}
