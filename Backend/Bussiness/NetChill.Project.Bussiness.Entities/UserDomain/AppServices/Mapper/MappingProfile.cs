using AutoMapper;
using NetChill.Project.Bussiness.Entities.MovieDomains.AppServices.DTOs;
using NetChill.Project.DataAccess.Domains.Domains;
using NetChill.Project.UserDomains.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChill.Project.UserDomains.AppServices.Mapper
{
    /// <summary>
    /// Mapping Profile
    /// </summary>
    public class MappingProfile1 : Profile
    {
        public MappingProfile1() : base("MappingProfile")
        {
            CreateMap<UserDTO, UserDomain>().ReverseMap();
            CreateMap<MovieUser, MovieUserDTO>().ReverseMap();
        }
    }
}
