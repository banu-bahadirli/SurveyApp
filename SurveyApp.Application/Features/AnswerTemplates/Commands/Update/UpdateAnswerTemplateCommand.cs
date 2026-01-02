using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Features.AnswerTemplates.Commands.Update;
using SurveyApp.Application.Features.AnswerTemplates.Constants;
using SurveyApp.Application.Features.AnswerTemplates.Dtos;
using SurveyApp.Application.Features.AnswerTemplates.Rules;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.AnswerTemplates.Commands.Update
{
	public class UpdateAnswerTemplateCommand : IRequest<UpdatedAnswerTemplateResponse>
	{
		public int Id { get; set; }
		public string Name { get; set; }  // Şablon adı
		public int OptionCount { get; set; }  // 2-4 arası
		public List<AnswerOptionDto> Options { get; set; } = new List<AnswerOptionDto>();

		public class UpdateAnswerTemplateCommandHandler : IRequestHandler<UpdateAnswerTemplateCommand, UpdatedAnswerTemplateResponse>
		{
			private readonly IAnswerTemplateRepository _answerTemplateRepository;
			private readonly IMapper _mapper;
			private readonly AnswerTemplateBusinessRules _rules;

			public UpdateAnswerTemplateCommandHandler(
				IAnswerTemplateRepository answerTemplateRepository,
				IMapper mapper,
				AnswerTemplateBusinessRules rules)
			{
				_answerTemplateRepository = answerTemplateRepository;
				_mapper = mapper;
				_rules = rules;
			}

			public async Task<UpdatedAnswerTemplateResponse> Handle(UpdateAnswerTemplateCommand command, CancellationToken cancellationToken)
			{
				// Mevcut şablonu al
				var answerTemplate = await _answerTemplateRepository.GetAsync(c => c.Id == command.Id, include: c => c.Include(x => x.Options));

				if (answerTemplate == null)
					throw new Exception("Cevap şablonu bulunamadı");

				// Kuralları kontrol et
				await _rules.OptionCountMustBeBetween2And4(command.OptionCount);
				await _rules.OptionCountMustMatchOptions(command.OptionCount, command.Options.Count);

				// Şablon verilerini güncelle
				answerTemplate.Name = command.Name;
				answerTemplate.OptionCount = command.OptionCount;

				// Options güncelle
				answerTemplate.Options.Clear(); // Eski seçenekleri temizle
				answerTemplate.Options = command.Options
					.Select(o => new AnswerOption
					{
						Text = o.Text,
						Order = o.Order
					}).ToList();

				// Güncellenmiş şablonu kaydet
				var updatedTemplate = await _answerTemplateRepository.UpdateAsync(answerTemplate, cancellationToken);

				// Response map et
				var response = _mapper.Map<UpdatedAnswerTemplateResponse>(updatedTemplate);
				response.Message = AnswerTemplateMessages.AnswerTemplateUpdated;
				response.Success = true;

				return response;
			}
		}
	}
}
