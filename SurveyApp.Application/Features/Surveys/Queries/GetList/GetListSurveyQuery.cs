using AutoMapper;
using MediatR;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetList;

public class GetListSurveyQuery : IRequest<List<GetListSurveyResponse>>
{
	public string? SearchText { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }


	public class GetListSurveyQueryHandler
		: IRequestHandler<GetListSurveyQuery,List<GetListSurveyResponse>>
	{
		private readonly ISurveyRepository _surveyRepository;
		private readonly IMapper _mapper;

		public GetListSurveyQueryHandler(ISurveyRepository surveyRepository, IMapper mapper)
		{
			_surveyRepository = surveyRepository;
			_mapper = mapper;
		}

		public async Task<List<GetListSurveyResponse>> Handle(GetListSurveyQuery request, CancellationToken cancellationToken)
		{
			var surveys = await _surveyRepository.GetListNoPaginationAsync(
							predicate: s =>
								(
									string.IsNullOrEmpty(request.SearchText) ||
									s.Title.StartsWith(request.SearchText) ||
									s.Description.StartsWith(request.SearchText)
								) &&
								(!request.StartDate.HasValue || s.StartDate >= request.StartDate.Value) &&
								(!request.EndDate.HasValue || s.EndDate <= request.EndDate.Value),
							cancellationToken: cancellationToken
						);

			var response = _mapper.Map<List<GetListSurveyResponse>>(surveys);
			return response;
		}
	}
}

