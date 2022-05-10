using System.Net.Http.Json;
using TodoList.Models;

namespace TodoListBlazor.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        public HttpClient _httpClient;
        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TaskDto> GetTaskDetail(string id)
        {
            var result=await _httpClient.GetFromJsonAsync<TaskDto>($"/api/task/{id}");
            return result;
        }

        public async Task<List<TaskDto>> GetTaskList()
        {
            var result =await _httpClient.GetFromJsonAsync<List<TaskDto>>("/api/task");
            return result;
        }
    }
}
