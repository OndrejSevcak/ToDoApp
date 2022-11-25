namespace Sample_ToDo_WebApp.Services
{
    public class APIService : IAPIService
    {
        private readonly IHttpClientFactory _httpClient;

        public APIService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendAsync(string url, HttpMethod method)
        {
            //client
            var client = _httpClient.CreateClient();

            //header
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //request
            var requestMessage = new HttpRequestMessage(method, url);

            //Send request
            var response = await client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return json;
            }
            else
            {
                throw new Exception($"Http status code {response.StatusCode}");
            }
        }

        public async Task<string> PostAsync(string url)
        {
            //client
            var client = _httpClient.CreateClient();

            //header
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //request
            //var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            //Send request
            var response = await client.PostAsync(url, null);
            
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return json;
            }
            else
            {
                throw new Exception($"Http status code {response.StatusCode}");
            }
        }
    }
}
