using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.Surveys.Constants;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.Delete;
public class DeleteSurveyCommand : IRequest<DeletedSurveyResponse>
{
	public int Id { get; set; }
	public class DeleteSurveyCommandHandler : IRequestHandler<DeleteSurveyCommand, DeletedSurveyResponse>
	{
		private readonly ISurveyRepository _surveyRepository;
		private readonly IMapper _mapper;

		public DeleteSurveyCommandHandler(ISurveyRepository surveyRepository, IMapper mapper)
		{
			_surveyRepository = surveyRepository;
			_mapper = mapper;
		}
		public async Task<DeletedSurveyResponse> Handle(DeleteSurveyCommand request, CancellationToken cancellationToken)
		{
			Survey? survey = await _surveyRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
			await _surveyRepository.DeleteAsync(survey,true);
			DeletedSurveyResponse response = _mapper.Map<DeletedSurveyResponse>(survey);
			response.Message=SurveyMessages.SurveyDeleted;
			response.Success=true;
			return response;
		}
	}
}
