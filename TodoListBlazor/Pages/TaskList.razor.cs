using Microsoft.AspNetCore.Components;
using TodoList.Models;
using TodoListBlazor.Services;
using System.Collections.Generic;
using TodoList.Models.Enumerable;

namespace TodoListBlazor.Pages
{
    public partial class TaskList
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }
        [Inject] private IUserApiClient userApiClient { get; set; }
        private List<AssigneeDto> Assignees;
        private List<TaskDto> Tasks { get; set; }
        private TaskListSearch TaskListSearch = new TaskListSearch();
        protected override async Task OnInitializedAsync()
        {
            Tasks = await TaskApiClient.GetTaskList();
            Assignees = await userApiClient.GetAssignee();
        }
    }
    public class TaskListSearch
    {
        public string Name { get; set; }
        public Guid AssigneeId { get; set; }
        public Priority Priority { get; set; }
    }
}
