
// Giúp chúng ta lặp bản dồ từ đối tượng này sang đối tượng khác
// Ánh xạ từ đối tượng này sang đối tượng khác
using API.DTOs;
using API.Entites;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
        }
    }
}