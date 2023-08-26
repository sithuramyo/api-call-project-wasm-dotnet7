using WeatherApp.Model;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WeatherApp.Pages
{
    public partial class FlightRador
    {
        private int pageNo = 1;
        private int pageSize = 10;
        private int pageCount = 0;
        private bool isLoading = false;
        private ApiResponse flights = new ApiResponse();
        private List<Row> flightsModel = new List<Row>();
        protected override async Task OnInitializedAsync()
        {
           
            await List(pageNo, pageSize);
            
        }
        private async Task PageChanged(int i)
        {
            pageNo = i;
            await List(pageNo, pageSize);
        }
        private async Task List(int pageNo = 1, int pageSize = 10)
        {
            isLoading = true;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://flight-radar1.p.rapidapi.com/aircrafts/list"),
                Headers =
                {
                    { "X-RapidAPI-Key", "e7970cc974msh03cc44ee5e983d1p1224b7jsned45887b6efa" },
                    { "X-RapidAPI-Host", "flight-radar1.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                flights = JsonConvert.DeserializeObject<ApiResponse>(body);
                if (flights != null && flights.Rows.Count > 0)
                {
                    flightsModel = flights.Rows
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }
                int totalCount = flights.Rows.Count;
                pageCount = (totalCount + pageSize - 1) / pageSize;
                isLoading = false;
               // StateHasChanged();
            }
        }
    }
}
