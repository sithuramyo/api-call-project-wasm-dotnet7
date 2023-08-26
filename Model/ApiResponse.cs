using MudBlazor;
using WeatherApp.Pages;

namespace WeatherApp.Model
{
    public class ApiResponse
    {
        public int Version { get; set; }
        public List<Row> Rows { get; set; }
    }
}
