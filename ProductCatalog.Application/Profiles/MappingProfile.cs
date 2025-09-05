using AutoMapper;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Dormain;


namespace ProductCatalog.Application.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<GetUsersDto,User>().ReverseMap();
            CreateMap<RegisterUserDto,User>().ReverseMap();
        }
    }
}
