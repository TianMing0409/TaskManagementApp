using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Interfaces.Repositories;
using TaskManagementApp.Application.Wrappers;

namespace TaskManagementApp.Application.Features.TaskItems.Queries
{
    public class GetAllTaskItemQuery : IRequest<PagedResponse<IEnumerable<GetAllTaskItemViewModel>>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }

    public class GetAllTaskItemQueryHandler : IRequestHandler<GetAllTaskItemQuery, PagedResponse<IEnumerable<GetAllTaskItemViewModel>>>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;

        public GetAllTaskItemQueryHandler(ITaskItemRepository taskItemRespository, IMapper mapper)
        {
            _taskItemRepository = taskItemRespository;
            _mapper = mapper; 
        }

        public async Task<PagedResponse<IEnumerable<GetAllTaskItemViewModel>>> Handle(GetAllTaskItemQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllTaskItemParameter>(request);
            var task = await _taskItemRepository.GetPagedResponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var taskViewModel = _mapper.Map<IEnumerable<GetAllTaskItemViewModel>>(task);
            return new PagedResponse<IEnumerable<GetAllTaskItemViewModel>>(taskViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
