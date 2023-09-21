using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Application.Features.TaskItems.Queries.Model
{
    public class TaskItemQuery
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }

    }
}
