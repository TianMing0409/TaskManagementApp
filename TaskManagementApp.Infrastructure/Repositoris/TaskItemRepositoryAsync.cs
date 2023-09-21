using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.DTOS;
using TaskManagementApp.Application.Interfaces.Repositories;
using TaskManagementApp.Domain.Entities;
using TaskManagementApp.Infrastructure.Persistence.Contexts;

namespace TaskManagementApp.Infrastructure.Persistence.Repositoris
{
    public class TaskItemRepositoryAsync : GenericRepositoryAsync<TaskItem>, ITaskItemRepository
    {
        private readonly DbSet<TaskItem> _taskItems;

        public TaskItemRepositoryAsync(TaskManagementDbContext taskManagementDbContext) : base(taskManagementDbContext)
        { 
            _taskItems = taskManagementDbContext.Set<TaskItem>();
        }
    }
}
