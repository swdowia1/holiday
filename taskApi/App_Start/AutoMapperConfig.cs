using AutoMapper;
using System.Linq;
using taskApi.DAL;
using taskApi.VM;

namespace taskApi.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<Customer, CustomerVM>().ReverseMap();
                config.CreateMap<CustomerAddVM, Customer>().ReverseMap();

                config.CreateMap<User, UserHoliday>(MemberList.None)
           .ForMember(dest => dest.Id,
               opts => opts.MapFrom(src => src.Id))
           .ForMember(dest => dest.Daty,
               opts => opts.MapFrom(src => src.Calendars.Select(j => j.Day.ToString("yyyyMMdd"))));



            });
        }
    }
}