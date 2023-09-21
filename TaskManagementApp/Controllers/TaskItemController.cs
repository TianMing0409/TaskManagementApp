using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Application.DTOS;
using TaskManagementApp.Application.Features.TaskItems.Queries;
using TaskManagementApp.Application.Features.TaskItems.Queries.Model;
using TaskManagementApp.Application.Interfaces;

namespace TaskManagementApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        //Get All Task Async
        [HttpGet]
        public async Task<IActionResult> GetAllTaskAsync()
        { 
            var taskList = await _taskItemService.GetAllTasksAsync();
            return Ok(taskList);
        }

        //Get Task By Id Async
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskItemByIdAsync(int taskId)
        {
            try
            {
                var task = await _taskItemService.GetTaskByIdAsync(taskId);
                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the task.");
            }
        }

        //Create Task Async
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskItemRequest taskItem)
        { 
            var task = await _taskItemService.CreateTaskAsync(taskItem);
            return Ok(task);
        }

        //Update Task Async
        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTaskAsync(int taskId, TaskItemRequest taskItemRequest)
        {
            try 
            {
                await _taskItemService.UpdateTaskAsync(taskId, taskItemRequest);
                return Ok(new { Message = "Task updated successfully!" });         
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occur while updating the task.");
            }
        }

        //Delete Task Async
        [HttpDelete]
        public async Task<IActionResult> DeleteTaskAsync(int taskId)
        {
            try
            {
                await _taskItemService.DeleteTaskAsync(taskId);
                return Ok(new { Message = "Task deleted successfully!" });
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        //Get Item by apply filtering and sorting (Got Error)
        //[HttpGet]
        //public async Task<IActionResult> GetTasks([FromQuery] TaskItemQuery query)
        //{
        //    var task = await _taskItemService.GetTasksAsync(query);
        //    return Ok(task);
        //}
    }
}
