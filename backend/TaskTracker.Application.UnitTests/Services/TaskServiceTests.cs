using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Contracts.DateAndTime;
using TaskTracker.Application.Contracts.Identity;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Services;
using TaskTracker.Domain.TaskItems;

namespace TaskTracker.Application.UnitTests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repository = new();
        private readonly Mock<ICurrentUser> _currentUser = new();
        private readonly Mock<IDateTimeProvider> _dateTimeProvider = new();

        private TaskService CreateService()
            => new TaskService(_repository.Object, _currentUser.Object, _dateTimeProvider.Object);

        public TaskServiceTests()
        {
            _currentUser.Setup(x => x.UserId).Returns("user-1");
            _dateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2025, 1, 1));
        }

        private static TaskItem CreateTask(Guid? id = null, string userId = "user-1")
        {
            return new TaskItem(
                id ?? Guid.NewGuid(),
                "Title",
                "Description",
                TaskPriority.Medium,
                DateTime.UtcNow,
                false,
                userId,
                DateTime.UtcNow
            );
        }

        // --------------------
        // Create
        // --------------------

        [Fact]
        public async Task CreateAsync_Should_Create_Task_For_Current_User()
        {
            // Arrange
            var service = CreateService();

            TaskItem? capturedTask = null;
            _repository
                .Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
                .Callback<TaskItem>(t => capturedTask = t)
                .Returns(Task.CompletedTask);

            // Act
            var id = await service.CreateAsync(
                "My task",
                "Description",
                TaskPriority.High,
                null,
                false);

            // Assert
            id.ShouldNotBe(Guid.Empty);
            capturedTask.ShouldNotBeNull();
            capturedTask!.UserId.ShouldBe("user-1");
            capturedTask.Title.ShouldBe("My task");
            capturedTask.CreatedAt.ShouldBe(_dateTimeProvider.Object.Now);
        }

        // --------------------
        // Update
        // --------------------

        [Fact]
        public async Task UpdateAsync_Should_Update_Task_When_Found()
        {
            // Arrange
            var service = CreateService();
            var task = CreateTask();

            _repository
                .Setup(r => r.GetByIdAsync(task.Id, "user-1"))
                .ReturnsAsync(task);

            // Act
            await service.UpdateAsync(
                task.Id,
                "Updated",
                "Updated desc",
                TaskPriority.High,
                null,
                true);

            // Assert
            task.Title.ShouldBe("Updated");
            task.IsDone.ShouldBeTrue();

            _repository.Verify(r => r.UpdateAsync(task), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_When_Task_Not_Found()
        {
            // Arrange
            var service = CreateService();
            var id = Guid.NewGuid();

            _repository
                .Setup(r => r.GetByIdAsync(id, "user-1"))
                .ReturnsAsync((TaskItem?)null);

            // Act / Assert
            await Should.ThrowAsync<TaskNotFoundException>(() =>
                service.UpdateAsync(
                    id,
                    "Title",
                    null,
                    TaskPriority.Low,
                    null,
                    false));
        }

        // --------------------
        // Delete
        // --------------------

        [Fact]
        public async Task DeleteAsync_Should_Delete_Task_When_Found()
        {
            // Arrange
            var service = CreateService();
            var task = CreateTask();

            _repository
                .Setup(r => r.GetByIdAsync(task.Id, "user-1"))
                .ReturnsAsync(task);

            // Act
            await service.DeleteAsync(task.Id);

            // Assert
            _repository.Verify(
                r => r.DeleteAsync(task.Id, "user-1"),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Throw_When_Task_Not_Found()
        {
            // Arrange
            var service = CreateService();
            var id = Guid.NewGuid();

            _repository
                .Setup(r => r.GetByIdAsync(id, "user-1"))
                .ReturnsAsync((TaskItem?)null);

            // Act / Assert
            await Should.ThrowAsync<TaskNotFoundException>(() =>
                service.DeleteAsync(id));
        }

        // --------------------
        // GetById
        // --------------------

        [Fact]
        public async Task GetByIdAsync_Should_Return_Task_When_Found()
        {
            // Arrange
            var service = CreateService();
            var task = CreateTask();

            _repository
                .Setup(r => r.GetByIdAsync(task.Id, "user-1"))
                .ReturnsAsync(task);

            // Act
            var result = await service.GetByIdAsync(task.Id);

            // Assert
            result.ShouldBe(task);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Throw_When_Task_Not_Found()
        {
            // Arrange
            var service = CreateService();

            _repository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), "user-1"))
                .ReturnsAsync((TaskItem?)null);

            // Act / Assert
            await Should.ThrowAsync<TaskNotFoundException>(() =>
                service.GetByIdAsync(Guid.NewGuid()));
        }

        // --------------------
        // GetAll
        // --------------------

        [Fact]
        public async Task GetAllAsync_Should_Return_Tasks_For_Current_User()
        {
            // Arrange
            var service = CreateService();
            var tasks = new List<TaskItem>
        {
            CreateTask(),
            CreateTask()
        };

            _repository
                .Setup(r => r.GetAllAsync("user-1"))
                .ReturnsAsync(tasks);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            result.ShouldBe(tasks);
        }
    }

}
