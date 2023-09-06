using AutoMapper;
using NetChill.Project.Bussiness.Entities.MovieDomains.AppServices.DTOs;
using NetChill.Project.DataAccess.Domains.Domains;
using NetChill.Project.MovieDomains.AppServices.DTOs;
using NetChill.Project.UserDomains.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChill.Project.MovieDomains.AppServices.Mapper
{
    /// <summary>
    /// Mapping Profile
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile() : base("MappingProfile")
        {
            CreateMap<MovieDTO, MovieDomain>().ReverseMap();
            CreateMap<MovieUserDTO, MovieUser>().ReverseMap();
        }
    }
}
