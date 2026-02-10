using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Contracts.DateAndTime;
using TaskTracker.Application.Contracts.Identity;
using TaskTracker.Application.Exceptions;
using TaskTracker.Domain.TaskItems;

namespace TaskTracker.Application.Services
{
    public class TaskService(ITaskRepository taskItemRepository, ICurrentUser currentUser, IDateTimeProvider dateTimeProvider)
    {
        public async Task<Guid> CreateAsync(string title, string? description, TaskPriority priority, DateTime? dueDate, bool isDone)
        {
            var id = Guid.NewGuid();
            var taskItem = new TaskItem(id, title, description, priority, dueDate, isDone, currentUser.UserId, dateTimeProvider.Now);

            await taskItemRepository.AddAsync(taskItem);
            return id;
        }

        public async Task UpdateAsync(Guid id, string title, string? description, TaskPriority priority, DateTime? dueDate, bool isDone)
        {
            var taskItem = await taskItemRepository.GetByIdAsync(id, currentUser.UserId);
            if (taskItem == null)
            {
                throw new TaskNotFoundException(id);
            }
            taskItem.Update(title, description, priority, dueDate, isDone);
            await taskItemRepository.UpdateAsync(taskItem);
        }

        public async Task DeleteAsync(Guid id)
        {
            var taskItem = await taskItemRepository.GetByIdAsync(id, currentUser.UserId);
            if (taskItem == null)
            {
                throw new TaskNotFoundException(id);
            }
            await taskItemRepository.DeleteAsync(id, currentUser.UserId);
        }

        public async Task<TaskItem> GetByIdAsync(Guid id)
        {
            var taskItem = await taskItemRepository.GetByIdAsync(id, currentUser.UserId);
            if (taskItem == null)
            {
                throw new TaskNotFoundException(id);
            }
            return taskItem;
        }

        public async Task<IReadOnlyList<TaskItem>> GetAllAsync()
        {
            return await taskItemRepository.GetAllAsync(currentUser.UserId);
        }
    }
}
