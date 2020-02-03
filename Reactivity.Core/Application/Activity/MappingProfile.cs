using AutoMapper;
using Domain;

namespace Application.Activity
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Activity, ActivityDto>();
            CreateMap<UserActivity, AttendeeDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Image));
        }
    }
}
