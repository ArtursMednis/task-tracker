using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.TaskItems;
using TaskTracker.Persistance.DatabaseContext;
using TaskTracker.Persistance.Repositories;

namespace TaskTracker.Persistance.UnitTests.Repositories
{
    public class TaskRepositoryTests
    {
        private static TasksDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new TasksDbContext(options);
        }

        private static TaskItem CreateTask(
            Guid? id = null,
            string? userId = null,
            string title = "Task title")
        {
            return new TaskItem(
                id ?? Guid.NewGuid(),
                title,
                "Description",
                TaskPriority.Medium,
                DateTime.UtcNow.AddDays(1),
                false,
                userId ?? "user-1",
                DateTime.UtcNow
            );
        }

        [Fact]
        public async Task AddAsync_Should_Persist_Task()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new TaskRepository(context);
            var task = CreateTask();

            // Act
            await repository.AddAsync(task);

            // Assert
            context.Tasks.Count().ShouldBe(1);
            context.Tasks.Single().Id.ShouldBe(task.Id);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_Only_Tasks_For_User()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new TaskRepository(context);

            await repository.AddAsync(CreateTask(userId: "user-1"));
            await repository.AddAsync(CreateTask(userId: "user-1"));
            await repository.AddAsync(CreateTask(userId: "user-2"));

            // Act
            var tasks = await repository.GetAllAsync("user-1");

            // Assert
            tasks.Count.ShouldBe(2);
            tasks.All(t => t.UserId == "user-1").ShouldBeTrue();
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Task_When_User_Matches()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new TaskRepository(context);

            var task = CreateTask();
            await repository.AddAsync(task);

            // Act
            var result = await repository.GetByIdAsync(task.Id, task.UserId);

            // Assert
            result.ShouldNotBeNull();
            result!.Id.ShouldBe(task.Id);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null_When_User_Does_Not_Match()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new TaskRepository(context);

            var task = CreateTask(userId: "user-1");
            await repository.AddAsync(task);

            // Act
            var result = await repository.GetByIdAsync(task.Id, "user-2");

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async Task UpdateAsync_Should_Persist_Changes()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new TaskRepository(context);

            var task = CreateTask(title: "Old title");
            await repository.AddAsync(task);

            task.Update(
                title: "New title",
                description: "Updated",
                priority: TaskPriority.High,
                dueDate: null,
                isDone: true
            );

            // Act
            await repository.UpdateAsync(task);

            // Assert
            var updated = await context.Tasks.SingleAsync();
            updated.Title.ShouldBe("New title");
            updated.IsDone.ShouldBeTrue();
            updated.Priority.ShouldBe(TaskPriority.High);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Task_When_User_Matches()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new TaskRepository(context);

            var task = CreateTask();
            await repository.AddAsync(task);

            // Act
            await repository.DeleteAsync(task.Id, task.UserId);

            // Assert
            context.Tasks.ShouldBeEmpty();
        }

        [Fact]
        public async Task DeleteAsync_Should_Not_Remove_Task_When_User_Does_Not_Match()
        {
            // Arrange
            var context = CreateDbContext();
            var repository = new TaskRepository(context);

            var task = CreateTask(userId: "user-1");
            await repository.AddAsync(task);

            // Act
            await repository.DeleteAsync(task.Id, "user-2");

            // Assert
            context.Tasks.Count().ShouldBe(1);
        }
    }

}
