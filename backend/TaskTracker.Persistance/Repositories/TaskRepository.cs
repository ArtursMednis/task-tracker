using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain.TaskItems;
using TaskTracker.Persistance.DatabaseContext;
using TaskTracker.Persistance.Entities;
using TaskTracker.Persistance.Mapping;

namespace TaskTracker.Persistance.Repositories
{
    public class TaskRepository(TasksDbContext context) : ITaskRepository
    {
        public async Task AddAsync(TaskItem taskItem)
        {
            await context.Tasks.AddAsync(taskItem.ToEntity());
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, string userId)
        {
            context.Tasks.RemoveRange(context.Tasks.Where(t => t.Id == id && t.UserId == userId));
            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<TaskItem>> GetAllAsync(string userId)
        {
            var tasks = await context.Tasks
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .Select(t => t.ToDomain())
                .ToListAsync();

            return tasks;
        }

        public Task<TaskItem?> GetByIdAsync(Guid id, string userId)
        {
            return context.Tasks
                .AsNoTracking()
                .Where(t => t.Id == id && t.UserId == userId)
                .Select(t => t.ToDomain())
                .FirstOrDefaultAsync();
        }

        public Task UpdateAsync(TaskItem taskItem)
        {
            var entity = taskItem.ToEntity();

            DetachExistingEntity(entity);

            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }

        private void DetachExistingEntity(TaskEntity entity)
        {
            var tracked = context.ChangeTracker
                .Entries<TaskEntity>()
                .FirstOrDefault(e => e.Entity.Id == entity.Id);
            if (tracked != null)
                tracked.State = EntityState.Detached;
        }
    }
}
