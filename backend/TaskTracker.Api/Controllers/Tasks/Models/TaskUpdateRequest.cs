using TaskTracker.Domain.TaskItems;

namespace TaskTracker.Api.Controllers.Tasks.Models
{
    public class TaskUpdateRequest
    {
        public required string Title { get; set; } = null!;
        public string? Description { get; set; }

        /// <summary>
        /// Accepted values: Low, Medium, High
        /// </summary>
        public string Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsDone { get; set; }
    }
}
