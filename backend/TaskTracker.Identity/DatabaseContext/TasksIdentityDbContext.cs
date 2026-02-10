using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Identity.DatabaseContext
{
    public class TasksIdentityDbContext(DbContextOptions<TasksIdentityDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
    }
}
