using AutoMapper;
using SurveyApp.Application.Features.Questions.Commands.Create;
using SurveyApp.Application.Features.Questions.Commands.Update;
using SurveyApp.Application.Features.Questions.Queries.GetById;
using SurveyApp.Application.Features.Questions.Queries.GetList;
using SurveyApp.Domain.Entities;


namespace SurveyApp.Application.Features.Questions.Profiles
{
	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
			CreateMap<Question, CreateQuestionCommand>().ReverseMap();
			CreateMap<Question, CreatedQuestionResponse>().ReverseMap();
			CreateMap<Question, UpdateQuestionCommand>().ReverseMap();
			CreateMap<Question, UpdatedQuestionResponse>().ReverseMap();
			CreateMap<Question, GetListQuestionResponse>().ReverseMap();
			CreateMap<Question, GetByIdQuestionResponse>().ReverseMap();
	
		}
	}
}

