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
    public class DeleteTaskItemByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class DeleteTaskItemByIdCommandHandler : IRequestHandler<DeleteTaskItemByIdCommand, Response<int>>
        {
            private readonly ITaskItemRepository _taskItemRepository;
            public DeleteTaskItemByIdCommandHandler(ITaskItemRepository taskItemRepository)
            {
                _taskItemRepository = taskItemRepository;
            }
            public async Task<Response<int>> Handle(DeleteTaskItemByIdCommand command, CancellationToken cancellationToken)
            {
                var task = await _taskItemRepository.GetByIdAsync(command.Id);
                if (task == null) throw new ApiException($"Task Not Found.");
                await _taskItemRepository.DeleteAsync(task);
                return new Response<int>(task.TaskId);
            }
        }
    }
}
