using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Services.Repositories;


namespace SurveyApp.Application.Features.Questions.Queries.GetList
{
	public class GetListQuestionQuery : IRequest<List<GetListQuestionResponse>>
	{
		public string? SearchText { get; set; }
		public class GetListQuestionQueryHandler
			: IRequestHandler<GetListQuestionQuery, List<GetListQuestionResponse>>
		{
			private readonly IQuestionRepository _questionRespository;
			private readonly IMapper _mapper;

			public GetListQuestionQueryHandler(
				IQuestionRepository questionRespository,
				IMapper mapper)
			{
				_questionRespository = questionRespository;
				_mapper = mapper;
			}

			public async Task<List<GetListQuestionResponse>> Handle(GetListQuestionQuery request,CancellationToken cancellationToken)
			{
				var questions = await _questionRespository.GetListNoPaginationAsync(
					predicate: q =>
						string.IsNullOrEmpty(request.SearchText) ||
						q.Text.Contains(request.SearchText),
					include: q => q
						.Include(x => x.AnswerTemplate)
						.ThenInclude(t => t.Options),
					enableTracking: false,
					cancellationToken: cancellationToken
				);

				return _mapper.Map<List<GetListQuestionResponse>>(questions);
			}
		}
	}
}
