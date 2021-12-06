using AutoMapper;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            // National Park
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();

            // Trail
            CreateMap<Trail, TrailDto>().ReverseMap();
            CreateMap<Trail, TrailCreateDto>().ReverseMap(); 
            CreateMap<Trail, TrailUpdateDto>().ReverseMap();
        }
    }
}
