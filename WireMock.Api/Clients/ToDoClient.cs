using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WireMock.Api.Models;

namespace WireMock.Api.Clients
{
    public class ToDoClient : IToDoClient
    {
        private readonly HttpClient _httpClient;

        public ToDoClient(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("JsonPlaceHolder");
        }

        public async Task<IEnumerable<ToDo>> GetToDos()
        {
            var response = await _httpClient.GetAsync("/todos");

            return await response.Content.ReadFromJsonAsync<IEnumerable<ToDo>>();
        }

        public async Task<ToDo> GetToDo(int id)
        {
            var response = await _httpClient.GetAsync($"/todos/{id}");

            return await response.Content.ReadFromJsonAsync<ToDo>();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}