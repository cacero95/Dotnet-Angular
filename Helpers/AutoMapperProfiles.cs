using API.DTOS;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDTO>()
                .ForMember(
                    member => member.PhotoUrl,
                    opt => opt.MapFrom(
                        src => src.Photos.FirstOrDefault( photo => photo.IsMain )!.Url
                    )
                );
            CreateMap<Photo, PhotoDTO>();
        }
    }
}