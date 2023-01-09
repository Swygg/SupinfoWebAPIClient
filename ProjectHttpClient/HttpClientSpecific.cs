using System.Net.Http.Json;

namespace ProjectHttpClient
{
    public class HttpClientSpecific<Dto, CreateInput, UpdateInput>
    {
        private HttpClient _client;
        private string _baseUrl;
        private string _createPart;
        private string _updatePart;
        private string _deletePart;
        private string _readOnePart;
        private string _readAllPart;

        public HttpClientSpecific(string baseUrl,
            string createPart,
            string updatePart,
            string deletePart,
            string readOnePart,
            string readAllPart)
        {
            _client = new HttpClient();
            _baseUrl = baseUrl;
            _createPart = createPart;
            _updatePart = updatePart;
            _deletePart = deletePart;
            _readOnePart = readOnePart;
            _readAllPart = readAllPart;
        }
        public bool Create(CreateInput input)
        {
            var url = _baseUrl + _createPart;
            var response = _client.PostAsJsonAsync(url, input)
               .GetAwaiter()
               .GetResult();

            return response.IsSuccessStatusCode ? true : false;
        }
        public bool Update(UpdateInput input)
        {
            var url = _baseUrl + _updatePart;
            var response = _client.PutAsJsonAsync(url, input)
               .GetAwaiter()
               .GetResult();

            return response.IsSuccessStatusCode ? true : false;
        }
        public bool Delete(int id)
        {
            var url = _baseUrl + _deletePart + id;
            var response = _client.DeleteAsync(url)
               .GetAwaiter()
               .GetResult();

            return response.IsSuccessStatusCode ? true : false;
        }
        public Dto? ReadOne(int id)
        {
            var url = _baseUrl + _readOnePart+id;
            var response = _client.GetAsync(url)
               .GetAwaiter()
               .GetResult();

            if (response.IsSuccessStatusCode)
            {
                return response
                    .Content
                    .ReadAsAsync<Dto>()
                    .GetAwaiter().GetResult();
            }
            return default;
        }
        public List<Dto>? ReadAll()
        {
            var url = _baseUrl + _readAllPart;
            var response = _client.GetAsync(url)
               .GetAwaiter()
               .GetResult();

            if (response.IsSuccessStatusCode)
            {
                return response
                    .Content
                    .ReadAsAsync<List<Dto>>()
                    .GetAwaiter().GetResult();
            }
            return default;
        }
    }
}