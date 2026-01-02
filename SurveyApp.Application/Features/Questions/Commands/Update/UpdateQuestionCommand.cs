using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.Questions.Constants;
using SurveyApp.Application.Features.Surveys.Commands.Update;
using SurveyApp.Application.Services.Repositories;

namespace SurveyApp.Application.Features.Questions.Commands.Update
{
	public class UpdateQuestionCommand : IRequest<UpdatedQuestionResponse>
	{
		public int Id { get; set; }  // URL'den gelen survey id
		public string Text { get; set; } = string.Empty;        // Soru metni
		public int AnswerTemplateId { get; set; }
	}

	public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, UpdatedQuestionResponse>
	{
		private readonly IQuestionRespository _questionRespository;
		private readonly IMapper _mapper;


		public UpdateQuestionCommandHandler(
			IQuestionRespository questionRespository,
			IMapper mapper)
		{
			_questionRespository = questionRespository;
			_mapper = mapper;
		}

		public async Task<UpdatedQuestionResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
		{
			// Mevcut survey'i al
			var question = await _questionRespository.GetAsync(
				predicate: b => b.Id == request.Id,
				cancellationToken: cancellationToken
			);

			if (question == null)
				throw new Exception("Soru bulunamadı"); // Daha sonra özel NotFoundException kullanabilirsin

			// Gelen request verilerini mevcut survey'e map et
			_mapper.Map(request, question);

			// Güncellenmiş survey'i kaydet
			await _questionRespository.UpdateAsync(question, cancellationToken);

			// Response DTO'ya map et ve döndür
			var response = _mapper.Map<UpdatedQuestionResponse>(question);
			response.Success = true;
			response.Message = QuestionMessages.OuestioneUpdated;
			return response;
		}
	}
}
