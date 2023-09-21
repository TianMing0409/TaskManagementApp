using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Exceptions;
using TaskManagementApp.Application.Interfaces.Repositories;
using TaskManagementApp.Application.Wrappers;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Application.Features.TaskItems.Queries
{
    public class GetTaskItemByIdQuery : IRequest<Response<TaskItem>>
    {
        public int Id { get; set; }

        public class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, Response<TaskItem>>
        {
            private readonly ITaskItemRepository _taskItemRepository;
            public GetTaskItemByIdQueryHandler(ITaskItemRepository taskItemRepository)
            {
                _taskItemRepository = taskItemRepository;
            }

            public async Task<Response<TaskItem>> Handle(GetTaskItemByIdQuery query, CancellationToken cancellationToken)
            {
                var task = await _taskItemRepository.GetByIdAsync(query.Id);
                if (task == null)
                {
                    throw new ApiException($"Task Not Found.");
                }
                return new Response<TaskItem>(task);
            }
        }
    }
}
