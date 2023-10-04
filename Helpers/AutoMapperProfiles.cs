using API.DTOS;
using API.Extensions;
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
                ).ForMember(
                    member => member.Age,
                    opt => opt.MapFrom(
                        src => src.DateOfBirth.CalculateAge()
                    )
                );
            CreateMap<Photo, PhotoDTO>();
        }
    }
}