using TaskTracker.Domain.Exceptions;

namespace TaskTracker.Domain.TaskItems
{
    public class TaskItem
    {
        public Guid Id { get; }
        public string Title { get; private set; } = null!;
        public string? Description { get; private set; }
        public TaskPriority Priority { get; private set; }
        public DateTime? DueDate { get; private set; }
        public bool IsDone { get; private set; }
        public string UserId { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        public TaskItem(Guid id, string title, string? description, TaskPriority priority, DateTime? dueDate, bool isDone, string userId, DateTime createdAt)
        {
            Id = id;
            Title = string.IsNullOrWhiteSpace(title)
                ? throw new DomainException("Title cannot be empty")
                : title;

            Description = description;
            Priority = priority;
            DueDate = dueDate;
            IsDone = isDone;
            UserId =
                string.IsNullOrWhiteSpace(userId)
                ? throw new DomainException("UserId cannot be empty")
                : userId;

            CreatedAt = createdAt;
        }

        public void Update(string title, string? description, TaskPriority priority, DateTime? dueDate, bool isDone)
        {
            Title = string.IsNullOrWhiteSpace(title)
                ? throw new DomainException("Title cannot be empty")
                : title;

            Description = description;
            Priority = priority;
            DueDate = dueDate;
            IsDone = isDone;
        }
    }
}
