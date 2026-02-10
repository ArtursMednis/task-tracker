using TaskTracker.Domain.TaskItems;
using TaskTracker.Persistance.Entities;

namespace TaskTracker.Persistance.Mapping
{
    internal static class TaskMapper
    {
            public static TaskEntity ToEntity(this TaskItem task)
            {
                return new TaskEntity
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Priority = task.Priority,
                    DueDate = task.DueDate?.Date,
                    IsDone = task.IsDone,
                    UserId = task.UserId,
                    CreatedAt = task.CreatedAt
                };
            }
    
            public static TaskItem ToDomain(this TaskEntity entity)
            {
                return new TaskItem(
                    entity.Id, 
                    entity.Title, 
                    entity.Description, 
                    entity.Priority, 
                    entity.DueDate?.Date, 
                    entity.IsDone, 
                    entity.UserId, 
                    entity.CreatedAt);
        }
    }
}
