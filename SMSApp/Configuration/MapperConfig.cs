using AutoMapper;
using Events;
using SmsApp.Data;
using SmsApp.DTO;

namespace SmsApp.Configuration
{
    public class MapperConfig : Profile
    {

        public MapperConfig()
        {
            CreateMap<SmsRequest, SmsEvent>()
                .ForMember(dest => dest.Vendor!, opt => opt.MapFrom<CustomVendorResolver>());

            CreateMap<SmsEvent, ShortMessage>().ReverseMap();
        }
    }
}
