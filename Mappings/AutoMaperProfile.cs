using System;
using AutoMapper;
using dev_habit.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dev_habit.Mappings;


public class AutoMaperProfile : Profile
{
    public AutoMaperProfile()
    {
        CreateMap<Region, RegionDTO>().ReverseMap();
        CreateMap<CreateRegionDTO, Region>().ReverseMap();
        CreateMap<UpdateRegionDTO, Region>().ReverseMap();

    }
}
