using AutoMapper;
using HudsonLearning.DTOs;
using HudsonLearning.Entities;
using HudsonLearning.Extensions;

namespace HudsonLearning.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<MemberUpdateDto, AppUser>();
        }
    }
}
