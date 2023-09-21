using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.DTOS;
using TaskManagementApp.Application.Features.TaskItems.Commands;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            CreateMap<CreateTaskItemCommand, TaskItem>();
            CreateMap<CreateTaskItemRequest, TaskItem>();
        }
    }
}
