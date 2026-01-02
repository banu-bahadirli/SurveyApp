using AutoMapper;
using MediatR;
using SurveyApp.Application.Features.AnswerTemplates.Commands.Create;
using SurveyApp.Application.Features.AnswerTemplates.Constants;
using SurveyApp.Application.Features.AnswerTemplates.Dtos;
using SurveyApp.Application.Features.AnswerTemplates.Rules;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;

public class CreateAnswerTemplateCommand : IRequest<CreatedAnswerTemplateResponse>
{
	public string Name { get; set; } = null!;
	public int OptionCount { get; set; }
	public List<AnswerOptionDto> Options { get; set; } = new();

	public class CreateAnswerTemplateCommandHandler : IRequestHandler<CreateAnswerTemplateCommand, CreatedAnswerTemplateResponse>
	{
		private readonly IAnswerTemplateRepository _answerTemplateRepository;
		private readonly IMapper _mapper;
		private readonly AnswerTemplateBusinessRules _rules;

		public CreateAnswerTemplateCommandHandler(IAnswerTemplateRepository answerTemplateRepository, IMapper mapper, AnswerTemplateBusinessRules rules)
		{
			_answerTemplateRepository = answerTemplateRepository;
			_mapper = mapper;
			_rules = rules;
		}

		public async Task<CreatedAnswerTemplateResponse> Handle(CreateAnswerTemplateCommand request, CancellationToken cancellationToken)
		{
			await _rules.OptionCountMustBeBetween2And4(request.OptionCount);
			await _rules.OptionCountMustMatchOptions(request.OptionCount, request.Options.Count);

			var entity = new AnswerTemplate
			{
				Name = request.Name,
				OptionCount = request.OptionCount,
				Options = request.Options.Select(o => new AnswerOption { Text = o.Text, Order = o.Order }).ToList()
			};

			var created = await _answerTemplateRepository.AddAsync(entity, cancellationToken);
			var response = _mapper.Map<CreatedAnswerTemplateResponse>(created);
			response.Message = AnswerTemplateMessages.AnswerTemplateCreated;
			response.Success = true;
			return _mapper.Map<CreatedAnswerTemplateResponse>(created);
		}
	}
}
