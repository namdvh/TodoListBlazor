using System.Net.Http.Json;
using TodoList.Models;

namespace TodoListBlazor.Services
{
    public class UserApiClient : IUserApiClient
    {
        public HttpClient _httpClient;
        public UserApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<AssigneeDto>> GetAssignee()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AssigneeDto>>($"/api/users");
            return result;
        }
    }
}
