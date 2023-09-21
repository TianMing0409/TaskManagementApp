using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Interfaces.Repositories;
using TaskManagementApp.Application.Wrappers;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Application.Features.TaskItems.Commands
{
    public partial class CreateTaskItemCommand : IRequest<Response<int>>
    {
        public string title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

    }

    public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, Response<int>>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;
        public CreateTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<TaskItem>(request);
            await _taskItemRepository.AddAsync(task);
            return new Response<int>(task.TaskId);
        }

    }
}
