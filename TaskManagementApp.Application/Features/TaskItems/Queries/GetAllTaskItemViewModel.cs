using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Application.Features.TaskItems.Queries
{
    public class GetAllTaskItemViewModel
    {
        public int TaskId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public string status { get; set; } = string.Empty;
    }
}
