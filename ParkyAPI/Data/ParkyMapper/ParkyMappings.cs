namespace ParkyAPI.Data.ParkyMapper
{
    using AutoMapper;

    using ParkyAPI.Models;
    using ParkyAPI.Models.Dtos;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trial, TrialDto>().ReverseMap();
            CreateMap<Trial, TrialUpdateDto>().ReverseMap();
            CreateMap<Trial, TrialInsertDto>().ReverseMap();
        }
    }
}
