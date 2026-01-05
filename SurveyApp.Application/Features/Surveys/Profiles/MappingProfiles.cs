using AutoMapper;
using SurveyApp.Application.Features.Surveys.Commands.Create;
using SurveyApp.Application.Features.Surveys.Commands.Delete;
using SurveyApp.Application.Features.Surveys.Commands.Update;
using SurveyApp.Application.Features.Surveys.Queries.GetById;
using SurveyApp.Application.Features.Surveys.Queries.GetList;
using SurveyApp.Application.Features.Surveys.Queries.GetNotCompletedSurveyUser;
using SurveyApp.Application.Features.Surveys.Queries.GetUserActiveSurveys;
using SurveyApp.Application.Features.Surveys.Queries.GetUserSurveyAnswers;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.Surveys.Profiles
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<CreateSurveyCommand, Survey>().ReverseMap();
			CreateMap<UpdateSurveyCommand, Survey>().ReverseMap();
			CreateMap<DeleteSurveyCommand, Survey>().ReverseMap();

			CreateMap<Survey, CreatedSurveyResponse>().ReverseMap();
			CreateMap<Survey, UpdatedSurveyResponse>().ReverseMap();
			CreateMap<Survey, DeletedSurveyResponse>().ReverseMap();

			CreateMap<Survey, GetByIdSurveyResponse>()
				.ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
				.ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToString("yyyy-MM-dd")));

			CreateMap<Survey, GetListSurveyResponse>()
				.ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
				.ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToString("yyyy-MM-dd")));

			CreateMap<UserSurvey, GetUserActiveSurveyResponse>()
				.ForMember(dest => dest.SurveyId, opt => opt.MapFrom(src => src.Survey != null ? src.Survey.Id : 0))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Survey != null ? src.Survey.Title : string.Empty))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Survey != null ? src.Survey.Description : string.Empty))
				.ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Survey != null ? src.Survey.StartDate : default))
				.ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Survey != null ? src.Survey.EndDate : default));

			CreateMap<UserSurvey, GetCompletedSurveyUserResponse>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User != null ? src.User.Id : 0))
				.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User != null ? src.User.FirstName : string.Empty))
				.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User != null ? src.User.LastName : string.Empty))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User != null ? src.User.Email : string.Empty));

			CreateMap<UserSurvey, GetNotCompletedSurveyUserResponse>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User != null ? src.User.Id : 0))
				.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User != null ? src.User.FirstName : string.Empty))
				.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User != null ? src.User.LastName : string.Empty))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User != null ? src.User.Email : string.Empty));

			CreateMap<UserSurveyAnswer, GetUserSurveyAnswerResponse>()
				.ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
				.ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Question != null ? src.Question.Text : string.Empty))
				.ForMember(dest => dest.AnswerText, opt => opt.MapFrom(src => string.Empty)) // metin cevabı yoksa boş
				.ForMember(dest => dest.AnswerOptionId, opt => opt.MapFrom(src => (int?)src.SelectedOptionId))
				.ForMember(dest => dest.AnswerOptionText, opt => opt.MapFrom(src => src.SelectedOption != null ? src.SelectedOption.Text : null));
		}
	}
}
