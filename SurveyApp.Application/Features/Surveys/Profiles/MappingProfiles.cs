using AutoMapper;
using SurveyApp.Application.Features.Surveys.Commands.Create;
using SurveyApp.Application.Features.Surveys.Commands.Delete;
using SurveyApp.Application.Features.Surveys.Commands.Update;
using SurveyApp.Application.Features.Surveys.Queries.GetById;
using SurveyApp.Application.Features.Surveys.Queries.GetList;
using SurveyApp.Application.Features.Surveys.Queries.GetUserActiveSurveys;
using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.Surveys.Profiles
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Survey, CreateSurveyCommand>().ReverseMap();
			CreateMap<Survey, CreatedSurveyResponse>().ReverseMap();

			CreateMap<Survey, UpdateSurveyCommand>().ReverseMap();
			CreateMap<Survey, UpdatedSurveyResponse>().ReverseMap();

			CreateMap<Survey, DeleteSurveyCommand>().ReverseMap();
			CreateMap<Survey, DeletedSurveyResponse>().ReverseMap();

			CreateMap<Survey, GetByIdSurveyResponse>()
				.ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
				.ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToString("yyyy-MM-dd")));

			CreateMap<Survey, GetListSurveyResponse>()
				.ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
				.ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToString("yyyy-MM-dd")));

			CreateMap<Paginate<Survey>, Paginate<GetListSurveyResponse>>();

			CreateMap<UserSurvey, GetUserActiveSurveyResponse>()
				.ForMember(dest => dest.SurveyId, opt => opt.MapFrom(src => src.Survey!.Id))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Survey!.Title))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Survey!.Description))
				.ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Survey!.StartDate))
				.ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Survey!.EndDate));

			CreateMap<UserSurvey, GetCompletedSurveyUserResponse>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User!.Id))
				.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User!.FirstName))
				.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User!.LastName))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User!.Email));

		}
	}
}
