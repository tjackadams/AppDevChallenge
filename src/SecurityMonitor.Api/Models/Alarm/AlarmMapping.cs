using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMonitor.Api.Models.Alarm
{
    public class AlarmMapping : Profile
    {
        public AlarmMapping()
        {
            CreateMap<Core.Models.Alarm, AlarmViewModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
