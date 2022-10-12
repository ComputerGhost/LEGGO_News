using APIClient.DTOs;
using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace APIClient.Connections.Http
{
    public class Connection<Details, SaveData, Summary> : IConnection<Details, SaveData, Summary>
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _endpoint;

        protected Connection(HttpClient httpClient, Uri endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<Details> CreateAsync(SaveData data)
        {
            var content = SerializePayload(data);
            var response = await _httpClient.PostAsync(_endpoint, content);
            response.EnsureSuccessStatusCode();
            return await DeserializeResponse<Details>(response);
        }

        public async Task DeleteAsync(long id)
        {
            var uri = GetResourceLocation(id);
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Details> FetchAsync(long id)
        {
            var uri = GetResourceLocation(id);
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await DeserializeResponse<Details>(response);
        }

        public async Task<SearchResults<Summary>> ListAsync(SearchParameters query)
        {
            var uri = new Uri(_endpoint.ToString() +
                $"?Key={query.Key}" +
                $"&Offset={query.Offset}" + 
                $"&Count={query.Count}" +
                $"&Query={HttpUtility.UrlEncode(query.Query)}",
                UriKind.RelativeOrAbsolute
            );
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await DeserializeResponse<SearchResults<Summary>>(response);
        }

        public async Task UpdateAsync(long id, SaveData data)
        {
            var uri = GetResourceLocation(id);
            var content = SerializePayload(data);
            var response = await _httpClient.PutAsync(uri, content);
            response.EnsureSuccessStatusCode();
        }


        private Uri GetResourceLocation(long id)
        {
            return new Uri($"{_endpoint}{id}", UriKind.RelativeOrAbsolute);
        }

        private StringContent SerializePayload<T>(T data)
        {
            var payload = JsonConvert.SerializeObject(data);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var responsePayload = JsonConvert.DeserializeObject<T>(responseString);
            if (responsePayload == null)
            {
                throw new InvalidDataException("Response message should not be empty.");
            }
            return responsePayload;
        }

    }
}
