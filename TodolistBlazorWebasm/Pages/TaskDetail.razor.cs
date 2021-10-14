using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Share;
using TodolistBlazorWebasm.Services;

namespace TodolistBlazorWebasm.Pages
{
    public partial class TaskDetail
    {
        [Inject] private ITaskApiClient taskApiClient { set; get; }

        [Parameter]
        public string TaskId { set; get; }

        private TodoTaskDto  taskDetails {set;get;}
     
        protected override async Task OnInitializedAsync()
        {
            taskDetails = await taskApiClient.GetTaskDetail(TaskId);
        }
    }

}
