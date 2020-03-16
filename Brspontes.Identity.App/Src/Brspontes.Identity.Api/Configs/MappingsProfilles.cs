using AutoMapper;
using Brspontes.Identity.Api.Dto;
using Brspontes.Identity.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brspontes.Identity.Api.Configs
{
    public class MappingsProfilles : Profile
    {
        public MappingsProfilles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}
