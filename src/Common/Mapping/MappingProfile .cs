
using AutoMapper;
using Kashmir.Captain.Server.Application.DTO;
using Kashmir.Captain.Server.Common.Extensions;
using Kashmir.Captain.Server.Infrastructure.Persistance.Entities;

namespace Kashmir.Captain.Server.Common
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDto>();
			//  .ForMember(dest => dest.DestinationProperty, opt => opt.MapFrom(src => src.SourceProperty));
			CreateMap<PagedList<User>, PagedList<UserDto>>();
			CreateMap<Tool, ToolDto>();
		}
	}

}
