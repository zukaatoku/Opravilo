using AutoMapper;
using Opravilo.API.Models.Responses.User;
using Opravilo.Application.Models.User;

namespace Opravilo.API.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserModel, UserResponse>();
        }
    }
}