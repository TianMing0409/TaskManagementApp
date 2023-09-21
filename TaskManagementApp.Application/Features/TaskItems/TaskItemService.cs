using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.DTOS;
using TaskManagementApp.Application.Features.TaskItems.Queries.Model;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Application.Interfaces.Repositories;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Application.Features.TaskItems
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public TaskItemService(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<CreateTaskItemRequest> CreateTaskAsync(CreateTaskItemRequest request)
        {
            var task = new TaskItem {
                TaskId = request.TaskId,
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                status = request.status
                };
            await _taskItemRepository.AddAsync(task);
            return request;
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _taskItemRepository.GetByIdAsync(id); 
            await _taskItemRepository.DeleteAsync(task);
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            var taskList = await _taskItemRepository.GetAllAsync();
            return taskList.ToList();
      
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            var task = await _taskItemRepository.GetByIdAsync(id);
            return task;
        }

        public async Task<List<TaskItem>> GetTasksAsync(TaskItemQuery query)
        {
            var tasks = await _taskItemRepository.GetAllAsync();

            //Filtering
            if (!string.IsNullOrEmpty(query.Title))
            { 
                tasks.Where(t => t.Title.Contains(query.Title)).ToList();
            }
            if (query.DueDate.HasValue)
            { 
                tasks = tasks.Where(t => t.DueDate == query.DueDate.Value).ToList();
            }
            if (!string.IsNullOrEmpty(query.Description))
            {
                tasks.Where(t => t.Description.Contains(query.Description)).ToList();
            }
            if (!string.IsNullOrEmpty(query.Status))
            { 
                tasks.Where(t => t.status.Contains(query.Status)).ToList();
            }

            //Sorting
            if(!string.IsNullOrEmpty(query.SortBy))
            {
                switch(query.SortBy)
                {
                    case "Title":
                        tasks = query.SortDesc ? tasks.OrderByDescending(t => t.Title).ToList() : tasks.OrderBy(t => t.Title).ToList();
                        break;
                    case "DueDate":
                        tasks = query.SortDesc ? tasks.OrderByDescending(t => t.DueDate).ToList() : tasks.OrderBy(t => t.DueDate).ToList();
                        break;
                    case "Desccription":
                        tasks = query.SortDesc ? tasks.OrderByDescending(t => t.Description).ToList() : tasks.OrderBy(t => t.Description).ToList();
                        break;
                    case "Status":
                        tasks = query.SortDesc ? tasks.OrderByDescending(t => t.status).ToList() : tasks.OrderBy(t => t.status).ToList();
                        break;
                }
            }
            return tasks.ToList();
        }

        public async Task UpdateTaskAsync(int id, TaskItemRequest taskItemRequest)
        {
            var existingTask = await _taskItemRepository.GetByIdAsync(id);

            if (existingTask != null)
            {
                existingTask.Title = taskItemRequest.Title;
                existingTask.Description = taskItemRequest.Description;
                existingTask.DueDate = taskItemRequest.DueDate;
                existingTask.status = taskItemRequest.Status;

                await _taskItemRepository.UpdateAsync(existingTask);
            }
        }
    }
}
