using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Application.Exceptions
{
    public class TaskNotFoundException(Guid taskId) : Exception($"Task with id {taskId} not found.")
    {

    }
}
