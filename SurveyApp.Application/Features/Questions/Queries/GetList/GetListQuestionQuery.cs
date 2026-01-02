using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Application.Features.Questions.Queries.GetList.Dtos;


namespace SurveyApp.Application.Features.Questions.Queries.GetList
{
	public class GetListQuestionQuery : IRequest<List<GetListQuestionResponse>>
	{
		public class GetListQuestionQueryHandler
			: IRequestHandler<GetListQuestionQuery, List<GetListQuestionResponse>>
		{
			private readonly IQuestionRespository _questionRespository;
			private readonly IMapper _mapper;

			public GetListQuestionQueryHandler(
				IQuestionRespository questionRespository,
				IMapper mapper)
			{
				_questionRespository = questionRespository;
				_mapper = mapper;
			}

			public async Task<List<GetListQuestionResponse>> Handle(
				GetListQuestionQuery request,
				CancellationToken cancellationToken)
			{
				// Include ile AnswerTemplate ve Options'ları da getiriyoruz
				var questions = await _questionRespository.GetListNoPaginationAsync(
					include: q => q.Include(x => x.AnswerTemplate)
								   .ThenInclude(t => t.Options),
					enableTracking: false,
					cancellationToken: cancellationToken
				);

				// AutoMapper ile DTO'ya map
				return _mapper.Map<List<GetListQuestionResponse>>(questions);
			}
		}
	}
}
