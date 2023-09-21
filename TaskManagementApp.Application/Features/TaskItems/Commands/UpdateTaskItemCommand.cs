using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Exceptions;
using TaskManagementApp.Application.Interfaces.Repositories;
using TaskManagementApp.Application.Wrappers;

namespace TaskManagementApp.Application.Features.TaskItems.Commands
{
    public class UpdateTaskItemCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string status { get; set; }

        public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand, Response<int>>
        {
            private readonly ITaskItemRepository _taskItemRepository;
            public UpdateTaskItemCommandHandler(ITaskItemRepository taskItemRepository)
            {
                _taskItemRepository = taskItemRepository;
            }
            public async Task<Response<int>> Handle(UpdateTaskItemCommand command, CancellationToken cancellationToken)
            {
                var task = await _taskItemRepository.GetByIdAsync(command.Id);

                if (task == null)
                {
                    throw new ApiException($"Task Not Found.");
                }
                else
                {
                    task.Title = command.Title;                 
                    task.Description = command.Description;
                    task.DueDate = command.DueDate;
                    task.status = command.status;

                    await _taskItemRepository.UpdateAsync(task);
                    return new Response<int>(task.TaskId);
                }
            }
        }
    }
}
