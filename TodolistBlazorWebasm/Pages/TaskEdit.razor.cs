using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Share;
using TodolistBlazorWebasm.Services;

namespace TodolistBlazorWebasm.Pages
{
    public partial class TaskEdit
    {
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private ITaskApiClient taskApiClient { get; set; }
        [Inject] private NavigationManager navigationManager { set; get; }

        [Parameter]
        public string TaskId { get; set; }
        private TodoTaskUpdateRequest Task ;


        protected async override Task OnInitializedAsync()
        {
            var taskDto = await taskApiClient.GetTaskDetail(TaskId);
            Task = new TodoTaskUpdateRequest();
            Task.Name = taskDto.Name;
            Task.Priority = taskDto.Priority;

        }
        private async Task EditTask(EditContext context)
        {
            var result = await taskApiClient.UpdateTask(Guid.Parse(TaskId), Task);
            if (result)
            {
                ToastService.ShowSuccess($"{Task.Name} has been Updated successfully", "Thành công");
                navigationManager.NavigateTo("/TaskList");
            }
            else
            {
                ToastService.ShowError("An error occured in progress", "Thất bại");
            }
        }
    }
}
