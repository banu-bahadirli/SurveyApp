using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.Surveys.Constants;
using SurveyApp.Application.Features.Surveys.Rules;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.Create
{
	public class CreateSurveyCommand : IRequest<CreatedSurveyResponse>
	{
		public string Title { get; init; } = null!;
		public string? Description { get; init; }
		public DateTime StartDate { get; init; }
		public DateTime EndDate { get; init; }
		public bool IsActive { get; init; }
		public List<int> QuestionIds { get; init; } = new();
		public List<int> UserIds { get; init; } = new();

		public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, CreatedSurveyResponse>
		{
			private readonly ISurveyRepository _surveyRepository;
			private readonly IUserSurveyRepository _userSurveyRepository;
			private readonly ISurveyQuestionRepository _surveyQuestionRepository;
			private readonly IMapper _mapper;
			private readonly SurveyBusinessRules _businessRules;

			public CreateSurveyCommandHandler(
				ISurveyRepository surveyRepository,
				IUserSurveyRepository userSurveyRepository,
				ISurveyQuestionRepository surveyQuestionRepository,
				IMapper mapper,
				SurveyBusinessRules businessRules)
			{
				_surveyRepository = surveyRepository;
				_userSurveyRepository = userSurveyRepository;
				_surveyQuestionRepository = surveyQuestionRepository;
				_mapper = mapper;
				_businessRules = businessRules;
			}

			public async Task<CreatedSurveyResponse> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
			{
				// Başlık benzersizliği kontrolü
				await _businessRules.SurveyTitleCannotBeDuplicatedWhenInserted(request.Title);

				// Survey entity oluştur
				var survey = _mapper.Map<Survey>(request);
				survey.CreatedDate = DateTime.UtcNow;

				var createdSurvey = await _surveyRepository.AddAsync(survey);

				// UserSurvey kayıtlarını ekle
				if (request.UserIds != null && request.UserIds.Count > 0)
				{
					foreach (var userId in request.UserIds)
					{
						var userSurvey = new UserSurvey
						{
							UserId = userId,
							SurveyId = createdSurvey.Id,
							IsCompleted = false,
							CreatedDate = DateTime.UtcNow
						};
						await _userSurveyRepository.AddAsync(userSurvey);
					}
				}

				// SurveyQuestion kayıtlarını ekle
				if (request.QuestionIds != null && request.QuestionIds.Count > 0)
				{
					foreach (var questionId in request.QuestionIds)
					{
						var surveyQuestion = new SurveyQuestion
						{
							SurveyId = createdSurvey.Id,
							QuestionId = questionId,
							CreatedDate = DateTime.UtcNow
						};
						await _surveyQuestionRepository.AddAsync(surveyQuestion);
					}
				}

				// Response oluştur
				var response = _mapper.Map<CreatedSurveyResponse>(createdSurvey);
				response.Success = true;
				response.Message = SurveyMessages.SurveyCreated;

				return response;
			}
		}
	}
}
