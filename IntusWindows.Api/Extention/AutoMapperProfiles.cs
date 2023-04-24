using AutoMapper;
using IntusWindows.Common.Models;
using IntusWindows.Core.Entities;

namespace IntusWindows.Api.Extention
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {            
            CreateMap<SubElement, SubElementDTO>();
            CreateMap<Window, WindowDTO>().ForMember
                (dest => dest.SubElements, opt=> opt.MapFrom(src=>src.SubElements)).
                AfterMap((src, dest) =>
                {
                    dest.NumberOfSubElements = dest.SubElements.Count();
                });

            CreateMap<Order, OrderDTO>().ForMember
                (dest => dest.Windows, opt => opt.MapFrom(src => src.Windows));

            CreateMap<SubElementDTO, SubElement>();
            CreateMap<OrderDTO, Order>().ForMember
               (dest => dest.Windows, opt => opt.MapFrom(src => src.Windows));
            CreateMap<WindowDTO, Window>().ForMember
               (dest => dest.SubElements, opt => opt.MapFrom(src => src.SubElements));
        }
    }
}
