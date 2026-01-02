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


public class GetListSurveyQuery : IRequest<Paginate<GetListSurveyResponse>>
{
	public int PageIndex { get; init; } = 0;
	public int PageSize { get; init; } = 10;

	public class GetListSurveyQueryHandler
		: IRequestHandler<GetListSurveyQuery, Paginate<GetListSurveyResponse>>
	{
		private readonly ISurveyRepository _surveyRepository;
		private readonly IMapper _mapper;

		public GetListSurveyQueryHandler(
			ISurveyRepository surveyRepository,
			IMapper mapper)
		{
			_surveyRepository = surveyRepository;
			_mapper = mapper;
		}

		public async Task<Paginate<GetListSurveyResponse>> Handle(
			GetListSurveyQuery request,
			CancellationToken cancellationToken)
		{
			var surveys = await _surveyRepository.GetListAsync(
				index: request.PageIndex,
				size: request.PageSize,
				cancellationToken: cancellationToken
			);

			var response = _mapper.Map<Paginate<GetListSurveyResponse>>(surveys);
			return response;
		}
	}
}
