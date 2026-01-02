using AutoMapper;
using MediatR;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetById;
public class GetByIdSurveyQuery : IRequest<GetByIdSurveyResponse>
{
	public int Id { get; set; }

	public class GetByIdSurveyQueryHandler : IRequestHandler<GetByIdSurveyQuery, GetByIdSurveyResponse>
	{
		private readonly ISurveyRepository _surveyRepository;
		private readonly IMapper _mapper;

		public GetByIdSurveyQueryHandler(ISurveyRepository surveyRepository, IMapper mapper)
		{
			_surveyRepository = surveyRepository;
			_mapper = mapper;
		}
		public async Task<GetByIdSurveyResponse> Handle(GetByIdSurveyQuery request, CancellationToken cancellationToken)
		{
			Survey? survey = await _surveyRepository.GetAsync(predicate: b => b.Id == request.Id, withDeleted: true, cancellationToken: cancellationToken);

			GetByIdSurveyResponse response = _mapper.Map<GetByIdSurveyResponse>(survey);

			return response;
		}
	}


}