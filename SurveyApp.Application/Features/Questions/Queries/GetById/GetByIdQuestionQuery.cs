using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Questions.Queries.GetById
{
	public class GetByIdQuestionQuery : IRequest<GetByIdQuestionResponse>
	{
		public int Id { get; set; }

		public class GetByIdCorporateCustomerQueryHandler : IRequestHandler<GetByIdQuestionQuery, GetByIdQuestionResponse>
		{
			private readonly IQuestionRespository _questionRespository;
			private readonly IMapper _mapper;

			public GetByIdCorporateCustomerQueryHandler(IQuestionRespository questionRespository,IMapper mapper)
			{
				_questionRespository = questionRespository;
				_mapper = mapper;
			}

			public async Task<GetByIdQuestionResponse> Handle(GetByIdQuestionQuery request, CancellationToken cancellationToken)
			{
				var question = await _questionRespository.GetAsync(
					c => c.Id == request.Id,
					include: q => q.Include(x => x.AnswerTemplate) // ⬅ burada ilişkiyi include ettik
				);

				return _mapper.Map<GetByIdQuestionResponse>(question);
			}

		}
	}
}
