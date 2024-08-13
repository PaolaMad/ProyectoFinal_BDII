using LOGIN.Services.Interfaces;

namespace LOGIN.Services
{
    public class APiSubscriberServices : IAPiSubscriberServices
    {
        private readonly HttpClient _httpClient;

        public APiSubscriberServices(HttpClient  httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<String> GetUserAsync()
        {
            var response = await _httpClient.GetAsync("https://669d2caa15704bb0e3055a1b.mockapi.io/Api/Abonado/Abonados");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
