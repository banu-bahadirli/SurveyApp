using AutoMapper;
using SurveyApp.Application.Features.AnswerTemplates.Commands.Create;
using SurveyApp.Application.Features.AnswerTemplates.Commands.Update;
using SurveyApp.Application.Features.AnswerTemplates.Dtos;
using SurveyApp.Application.Features.AnswerTemplates.Queries.GetById;
using SurveyApp.Application.Features.AnswerTemplates.Queries.GetList;
using SurveyApp.Domain.Entities;


namespace SurveyApp.Application.Features.AnswerTemplates.Profiles
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{

			CreateMap<AnswerTemplate, CreateAnswerTemplateCommand>().ReverseMap();
			CreateMap<AnswerTemplate, CreatedAnswerTemplateResponse>().ReverseMap();

			
			CreateMap<AnswerTemplate, UpdateAnswerTemplateCommand>().ReverseMap();
			CreateMap<AnswerTemplate, UpdatedAnswerTemplateResponse>().ReverseMap();

		
			CreateMap<AnswerTemplate, GetListAnswerTemplateResponse>()
				.ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options))
				.ReverseMap();

			CreateMap<AnswerTemplate, GetByIdAnswerTemplateResponse>()
				.ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options))
				.ReverseMap();

			
			CreateMap<AnswerOption, AnswerOptionDto>().ReverseMap();

			
			CreateMap<AnswerTemplate, AnswerTemplateDto>().ReverseMap();

		}
	}
}


