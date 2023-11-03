using NguyenHoangKhang_Test_API.Models;

namespace NguyenHoangKhang_Test_API.Mapper
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {

            CreateMap<WeatherAPIData, WeatherData>()
                .ForMember(
                    des => des.cityId,
                    opt => opt.MapFrom(src => src.id)
                )
                .ForMember(
                    des => des.cityName,
                    opt => opt.MapFrom(src => src.name)
                )
                .ForMember(
                    des => des.weatherMain,
                    opt => opt.MapFrom(src => src.weather[0].main)
                )
                .ForMember(
                    des => des.weatherDescription,
                    opt => opt.MapFrom(src => src.weather[0].description)
                )
                .ForMember(
                    des => des.weatherIcon,
                    opt => opt.MapFrom(src => "http://openweathermap.org/img/wn/" + src.weather[0].icon + "@2x.png")
                )
                .ForMember(
                    des => des.mainTemp,
                    opt => opt.MapFrom(src => src.main.temp)
                )
                .ForMember(
                    des => des.mainHumidity,
                    opt => opt.MapFrom(src => src.main.humidity)
                )
                .ReverseMap();
        }
    }
}
