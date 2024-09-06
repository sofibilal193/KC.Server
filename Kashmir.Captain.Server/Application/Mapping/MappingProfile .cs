
using AutoMapper;
using Kashmir.Captain.Server.Application.DTO;
using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Entities;

namespace Kashmir.Captain.Server.Application
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDto>();
			//  .ForMember(dest => dest.DestinationProperty, opt => opt.MapFrom(src => src.SourceProperty));
			CreateMap<PagedList<User>, PagedList<UserDto>>();
		}
	}

}
