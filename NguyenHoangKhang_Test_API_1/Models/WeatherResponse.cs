namespace NguyenHoangKhang_Test_API.Models
{
    public class WeatherResponse
    {
        public List<WeatherData> data { get; set; }
        public string message {  get; set; }
        public int statusCode {  get; set; }
    }
}
