using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Services.Repositories;

namespace SurveyApp.Application.Features.AnswerTemplates.Queries.GetById;

public class GetByIdAnswerTemplateQuery : IRequest<GetByIdAnswerTemplateResponse>
{
	public int Id { get; set; }

	public class GetByIdAnswerTemplateQueryHandler : IRequestHandler<GetByIdAnswerTemplateQuery, GetByIdAnswerTemplateResponse>
	{

		private readonly IAnswerTemplateRepository _answerTemplateRepository;
		private readonly IMapper _mapper;


		public GetByIdAnswerTemplateQueryHandler(IAnswerTemplateRepository answerTemplateRepository, IMapper mapper)
		{
			_answerTemplateRepository = answerTemplateRepository;
			_mapper = mapper;
		}

		public async Task<GetByIdAnswerTemplateResponse> Handle(GetByIdAnswerTemplateQuery request, CancellationToken cancellationToken)
		{
			var template = await _answerTemplateRepository
						.GetAsync(
							predicate: t => t.Id == request.Id,
							include: q => q.Include(t => t.Options));

			return _mapper.Map<GetByIdAnswerTemplateResponse>(template);
		}
	}
}