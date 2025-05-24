
using AutoMapper;
using KC.Application.DTO;
using KC.Common.Extensions;
using KC.Infrastructure.Persistance.Entities;

namespace KC.Common
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
