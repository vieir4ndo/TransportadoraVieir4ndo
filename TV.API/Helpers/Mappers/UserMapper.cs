using AutoMapper;
using TV.API.ViewModels;
using TV.DAL.Entities;

namespace TV.API.Helpers.Mappers
{
    public class UserMappers : Profile
    {
        public UserMappers()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}