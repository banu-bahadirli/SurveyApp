using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Features.AnswerTemplates.Queries.GetList;
using SurveyApp.Application.Services.Repositories;

namespace SurveyApp.Application.Features.AnswerTemplates.Queries.GetList;
public class GetListAnswerTemplateQuery : IRequest<List<GetListAnswerTemplateResponse>>
{
	public class GetListAnswerTemplateQueryHandler
	: IRequestHandler<GetListAnswerTemplateQuery, List<GetListAnswerTemplateResponse>>
	{
		private readonly IAnswerTemplateRepository _answerTemplateRepository;
		private readonly IMapper _mapper;

		public GetListAnswerTemplateQueryHandler(
			IAnswerTemplateRepository answerTemplateRepository,
			IMapper mapper)
		{
			_answerTemplateRepository = answerTemplateRepository;
			_mapper = mapper;
		}

		public async Task<List<GetListAnswerTemplateResponse>> Handle(GetListAnswerTemplateQuery request, CancellationToken cancellationToken)
		{
			var templates = await _answerTemplateRepository.GetListNoPaginationAsync(
				include: q => q.Include(t => t.Options),
				enableTracking: false,
				cancellationToken: cancellationToken
			);

			return _mapper.Map<List<GetListAnswerTemplateResponse>>(templates);
		}


	}


}



