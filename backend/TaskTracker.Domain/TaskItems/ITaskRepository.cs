using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.TaskItems
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id, string userId);
        Task<IReadOnlyList<TaskItem>> GetAllAsync(string userId);
        Task AddAsync(TaskItem taskItem);
        Task UpdateAsync(TaskItem taskItem);
        Task DeleteAsync(Guid id, string userId);
    }
}
