using System.Globalization;
using TaskTracker.Domain.TaskItems;

namespace TaskTracker.Api.Controllers.Tasks.Models
{
    public class TaskDetailsResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        /// <summary>
        /// Possible values: Low, Medium, High
        /// </summary>
        public string Priority { get; set; }
        public string? DueDate { get; set; }
        public bool IsDone { get; set; }
        public string CreatedAt { get; set; }

        public static TaskDetailsResponse FromDomain(TaskItem task)
        {
            return new TaskDetailsResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority.ToString(),
                DueDate = task.DueDate?.ToString(Formatting.ApiDateFormats.DateOnly, CultureInfo.InvariantCulture),
                IsDone = task.IsDone,
                CreatedAt = task.CreatedAt.ToString(Formatting.ApiDateFormats.DateOnly, CultureInfo.InvariantCulture)
            };
        }
    }
}
