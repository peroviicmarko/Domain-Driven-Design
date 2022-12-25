using AutoMapper;
using DDD.Application.DTOs.User;
using DDD.Domain.Models;

namespace DDD.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserBaseDto>().ReverseMap();
        }
    }
}
