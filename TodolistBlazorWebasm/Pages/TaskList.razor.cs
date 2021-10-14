
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Share;
using TodoList.Share.SeedWork;
using TodolistBlazorWebasm.Components;
using TodolistBlazorWebasm.Pages.Components;
using TodolistBlazorWebasm.Services;

namespace TodolistBlazorWebasm.Pages
{
    public partial class TaskList
    {
        [Inject] private ITaskApiClient taskApiClient { set; get; }
      
        protected Confirmation DeleteConfirmation { get; set; }
        protected AssignTask AssignTaskDialog { get; set; }

        private List<TodoTaskDto> TodoTasks { set; get; }

        private MetaData MetaData { set; get; }


        private TaskListSearch taskListSearch = new TaskListSearch();

        private Guid DeleteId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetTasks();
        }

        public async Task SearchTask(TaskListSearch task)
        {
            taskListSearch = task;
            await GetTasks();
        }

        public void OnDeleteTask(Guid deleteId)
        {
            DeleteId = deleteId;
            DeleteConfirmation.Show();
        }

        public async Task OnConfirmDeleteTask(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await taskApiClient.DeleteTask(DeleteId);
                await GetTasks();
            }
        }
        public void OpenAssignPopup(Guid id)
        {
            AssignTaskDialog.Show(id);
        }
        public async Task AssignTaskSuccess(bool result)
        {
            if(result)
            {
                await GetTasks();
            }
        }

        private async Task GetTasks()
        {
            var pagingResponse = await taskApiClient.GetTaskList(taskListSearch);
            TodoTasks = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SelectedPage(int page)
        {
            taskListSearch.PageNumber = page;
            await GetTasks();
        }
    }
}
