using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWebAPI.Entities;

namespace UserWebAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, AccountModel>();
            CreateMap<AccountModel, User>();            
        }
    }
}
