using System.Globalization;
using TaskTracker.Domain.TaskItems;

namespace TaskTracker.Api.Controllers.Tasks.Models
{
    public class TaskListResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// Possible values: Low, Medium, High
        /// </summary>
        public string Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsDone { get; set; }
        public string CreatedAt { get; set; }

        public static TaskListResponse FromDomain(TaskItem task)
        {
            return new TaskListResponse
            {
                Id = task.Id,
                Title = task.Title,
                Priority = task.Priority.ToString(),
                DueDate = task.DueDate,
                IsDone = task.IsDone,
                CreatedAt = task.CreatedAt.ToString(Formatting.ApiDateFormats.DateOnly, CultureInfo.InvariantCulture)
            };
        }
    }
}
