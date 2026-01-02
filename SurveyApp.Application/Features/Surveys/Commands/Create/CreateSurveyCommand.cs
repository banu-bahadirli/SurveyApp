using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.Surveys.Constants;
using SurveyApp.Application.Features.Surveys.Rules;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Security.Hashing;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.Create
{
	public class CreateSurveyCommand : IRequest<CreatedSurveyResponse>//,ISecuredRequest
	{
		public string Title { get; init; } = null!;
		public string? Description { get; init; }

		public DateTime StartDate { get; init; }
		public DateTime EndDate { get; init; }
		public bool IsActive { get; init; }
		public List<int> QuestionIds { get; init; } = new();
		public List<int> UserIds { get; init; } = new();

		//public string[] Roles => new[] { "User" };

		public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, CreatedSurveyResponse>
		{
			private readonly ISurveyRepository _surveyRepository;
			private readonly IMapper _mapper;
			private readonly SurveyBusinessRules _businessRules;

			public CreateSurveyCommandHandler(
				ISurveyRepository surveyRepository,
				IMapper mapper,
				SurveyBusinessRules businessRules)
			{
				_surveyRepository = surveyRepository;
				_mapper = mapper;
				_businessRules = businessRules;
			}

			public async Task<CreatedSurveyResponse> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
			{
				await _businessRules.SurveyTitleCannotBeDuplicatedWhenInserted(request.Title);			
				var survey = _mapper.Map<Survey>(request);
			    await _surveyRepository.AddAsync(survey);
				var response = _mapper.Map<CreatedSurveyResponse>(survey);
				response.Success = true;
				response.Message = SurveyMessages.SurveyCreated;
				return response;
			}
		}
	}
}
