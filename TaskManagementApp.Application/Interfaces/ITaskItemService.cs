using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.DTOS;
using TaskManagementApp.Application.Features.TaskItems.Queries.Model;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Application.Interfaces
{
    public interface ITaskItemService
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<CreateTaskItemRequest> CreateTaskAsync(CreateTaskItemRequest task);
        Task UpdateTaskAsync(int id, TaskItemRequest taskItemRequest);
        Task DeleteTaskAsync(int id);

        //Filtering and sorting 
        Task<List<TaskItem>> GetTasksAsync(TaskItemQuery query);
    }
}
