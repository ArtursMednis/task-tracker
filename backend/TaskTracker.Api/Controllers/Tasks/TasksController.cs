using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Controllers.Tasks.Models;
using TaskTracker.Application.Services;
using TaskTracker.Domain.TaskItems;

namespace TaskTracker.Api.Controllers.Tasks
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TasksController(TaskService taskService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TaskListResponse>>> GetAll()
        {
            var tasks = await taskService.GetAllAsync();
            var tasksResponse = tasks.Select(TaskListResponse.FromDomain).ToList();
            return Ok(tasksResponse);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<TaskDetailsResponse>> GetById(Guid id)
        {
            var task = await taskService.GetByIdAsync(id);
            var taskResponse = TaskDetailsResponse.FromDomain(task);
            return Ok(taskResponse);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TaskCreateRequest request)
        {
            var priority = Enum.Parse<TaskPriority>(request.Priority, ignoreCase: true);
            var taskId = await taskService.CreateAsync(request.Title, request.Description, priority, request.DueDate, request.IsDone);
            var temp = CreatedAtAction(nameof(GetById), new { id = taskId }, null);

            return CreatedAtAction(nameof(GetById), new { id = taskId }, null);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] TaskUpdateRequest request)
        {
            var priority = Enum.Parse<TaskPriority>(request.Priority, ignoreCase: true);
            await taskService.UpdateAsync(id, request.Title, request.Description, priority, request.DueDate, request.IsDone);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await taskService.DeleteAsync(id);
            return Ok();
        }
    }
}
