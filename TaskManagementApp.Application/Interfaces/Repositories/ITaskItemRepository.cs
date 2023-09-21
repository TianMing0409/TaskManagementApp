using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.DTOS;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Application.Interfaces.Repositories
{
    public interface ITaskItemRepository : IGenericRepositoryAsync<TaskItem>
    {
        //Other features (Pending)
    }
}
