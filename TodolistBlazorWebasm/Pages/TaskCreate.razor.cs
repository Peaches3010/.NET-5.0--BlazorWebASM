using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using TodoList.Share;
using TodolistBlazorWebasm.Services;

namespace TodolistBlazorWebasm.Pages
{
    public partial class TaskCreate
    {
        [Inject] private ITaskApiClient taskApiClient { set; get; }
        [Inject] private NavigationManager navigationManager { set; get; }
        [Inject] private IToastService ToastService { get; set; }
        private TodoTaskCreateRequest Task = new TodoTaskCreateRequest();

        private async Task SubmitTask(EditContext context)
        {
            var result = await taskApiClient.CreateTask(Task);

            if (result)
            {
                ToastService.ShowSuccess($"{Task.Name} has been created successfully","Thành công");
                navigationManager.NavigateTo("/TaskList");
            }
            else
            {
                ToastService.ShowError("An error occured in progress","Thất bại");
            }
        }
    }

}
