using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Exceptions;
using TaskTracker.Domain.TaskItems;

namespace TaskTracker.Domain.UnitTests.TaskItems
{
    public class TaskItemTests
    {
        private static TaskItem CreateValidTaskItem()
        {
            return new TaskItem(
                id: Guid.NewGuid(),
                title: "title",
                description: "description",
                priority: TaskPriority.Medium,
                dueDate: DateTime.UtcNow.AddDays(3),
                isDone: false,
                userId: "ad355d38-7c6f-48a1-ba44-7be1a97443c6",
                createdAt: DateTime.UtcNow
            );
        }

        [Fact]
        public void Constructor_Should_Create_TaskItem_When_Arguments_Are_Valid()
        {
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var createdAt = DateTime.UtcNow;

            var task = new TaskItem(
                id,
                "My task",
                "Some description",
                TaskPriority.High,
                DateTime.UtcNow.AddDays(1),
                isDone: false,
                userId,
                createdAt
            );

            task.Id.ShouldBe(id);
            task.Title.ShouldBe("My task");
            task.Description.ShouldBe("Some description");
            task.Priority.ShouldBe(TaskPriority.High);
            task.DueDate.ShouldNotBeNull();
            task.IsDone.ShouldBeFalse();
            task.UserId.ShouldBe(userId);
            task.CreatedAt.ShouldBe(createdAt);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_Should_Throw_When_Title_Is_Invalid(string invalidTitle)
        {
            var exception = Should.Throw<DomainException>(() =>
                new TaskItem(
                    Guid.NewGuid(),
                    invalidTitle!,
                    "description",
                    TaskPriority.Low,
                    dueDate: null,
                    isDone: false,
                    userId: "ad355d38-7c6f-48a1-ba44-7be1a97443c6",
                    DateTime.UtcNow
                ));

            exception.Message.ShouldBe("Title cannot be empty");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_Should_Throw_When_UserId_Is_Invalid(string invalidUserId)
        {
            var exception = Should.Throw<DomainException>(() =>
                new TaskItem(
                    Guid.NewGuid(),
                    "Valid title",
                    "description",
                    TaskPriority.Low,
                    dueDate: null,
                    isDone: false,
                    invalidUserId!,
                    DateTime.UtcNow
                ));

            exception.Message.ShouldBe("UserId cannot be empty");
        }

        [Fact]
        public void Update_Should_Modify_TaskItem_State()
        {
            var task = CreateValidTaskItem();
            var newDueDate = DateTime.UtcNow.AddDays(10);

            task.Update(
                title: "Updated title",
                description: "Updated description",
                priority: TaskPriority.High,
                dueDate: newDueDate,
                isDone: true
            );

            task.Title.ShouldBe("Updated title");
            task.Description.ShouldBe("Updated description");
            task.Priority.ShouldBe(TaskPriority.High);
            task.DueDate.ShouldBe(newDueDate);
            task.IsDone.ShouldBeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Update_Should_Throw_When_Title_Is_Invalid(string invalidTitle)
        {
            var task = CreateValidTaskItem();

            var exception = Should.Throw<DomainException>(() =>
                task.Update(
                    invalidTitle!,
                    "description",
                    TaskPriority.Medium,
                    dueDate: null,
                    isDone: false
                ));

            exception.Message.ShouldBe("Title cannot be empty");
        }
    }
}
