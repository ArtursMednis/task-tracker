using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.TaskItems;

namespace TaskTracker.Persistance.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsDone { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
