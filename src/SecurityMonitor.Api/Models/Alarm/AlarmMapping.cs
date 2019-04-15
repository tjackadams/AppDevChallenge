using AutoMapper;

namespace SecurityMonitor.Api.Models.Alarm
{
    public class AlarmMapping : Profile
    {
        public AlarmMapping()
        {
            CreateMap<Core.Models.Alarm, AlarmViewModel>()
                .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.DeviceId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
