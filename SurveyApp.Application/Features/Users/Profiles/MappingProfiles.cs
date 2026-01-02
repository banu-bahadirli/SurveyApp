using AutoMapper;
using SurveyApp.Application.Features.Users.Commands.Create;
using SurveyApp.Application.Features.Users.Queries.GetById;
using SurveyApp.Application.Features.Users.Queries.GetList;
using SurveyApp.Core.Security.Entities;
using SurveyApp.Domain.Entities;


namespace SurveyApp.Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{

		CreateMap<User, GetByIdUserQuery>().ReverseMap();
		CreateMap<User, GetByIdUserResponse>().ReverseMap();
		CreateMap<CreateUserCommand, User>()
			.ForMember(dest => dest.PasswordHash, opt => opt.Ignore())  // hash manuel set edilecek
			.ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())  // salt manuel set edilecek
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => true))  // default true
			.ForMember(dest => dest.Id, opt => opt.Ignore());
		CreateMap<User, CreatedUserResponse>();
		CreateMap<User,GetListUserResponse>();	

	}
}