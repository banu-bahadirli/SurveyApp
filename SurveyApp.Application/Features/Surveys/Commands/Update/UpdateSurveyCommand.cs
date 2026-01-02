using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Services.Repositories;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.Surveys.Commands.Update
{
	public class UpdateSurveyCommand : IRequest<UpdatedSurveyResponse>
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string? Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsActive { get; set; }

		// Formdan gelen kullanıcı id’leri
		public List<int> UserIds { get; set; } = new();
	}

	public class UpdateSurveyCommandHandler
		: IRequestHandler<UpdateSurveyCommand, UpdatedSurveyResponse>
	{
		private readonly ISurveyRepository _surveyRepository;
		private readonly IMapper _mapper;

		public UpdateSurveyCommandHandler(
			ISurveyRepository surveyRepository,
			IMapper mapper)
		{
			_surveyRepository = surveyRepository;
			_mapper = mapper;
		}

		public async Task<UpdatedSurveyResponse> Handle(
			UpdateSurveyCommand request,
			CancellationToken cancellationToken)
		{
			// Survey + UserSurveys birlikte alınmalı
			var survey = await _surveyRepository.GetAsync(
				s => s.Id == request.Id,
				include: q => q.Include(s => s.UserSurveys),
				cancellationToken: cancellationToken
			);

			if (survey == null)
				throw new Exception("Anket bulunamadı");

			// Temel alanları map et
			_mapper.Map(request, survey);

			// 🔴 Eski kullanıcı ilişkilerini temizle
			survey.UserSurveys.Clear();

			// 🟢 Yeni kullanıcı ilişkilerini ekle
			foreach (var userId in request.UserIds)
			{
				survey.UserSurveys.Add(new UserSurvey
				{
					SurveyId = survey.Id,
					UserId = userId
				});
			}

			await _surveyRepository.UpdateAsync(survey, cancellationToken);

			return _mapper.Map<UpdatedSurveyResponse>(survey);
		}
	}
}
