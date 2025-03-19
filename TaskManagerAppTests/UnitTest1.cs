using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.Tasks;
using TaskManagerApp.Services.Task;
using TaskManagerApp.Repository.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAppTests
{
    [TestFixture]
    public class TaskServiceTests
    {
        private TaskService _taskService;
        private Mock<ITaskRepository> _taskRepositoryMock;
        private Mock<ILogger<TaskService>> _loggerMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void SetUp()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _loggerMock = new Mock<ILogger<TaskService>>();
            _mapperMock = new Mock<IMapper>();
            _taskService = new TaskService(_taskRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task AddTaskAsync_ShouldReturnTrue_WhenTaskIsAdded()
        {
            // Arrange
            var taskDto = new CreateTaskDTO { Name = "Test Task" };
            var taskModel = new TaskModel { Name = "Test Task" };
            _mapperMock.Setup(m => m.Map<TaskModel>(taskDto)).Returns(taskModel);
            _taskRepositoryMock.Setup(r => r.AddTaskAsync(taskModel)).ReturnsAsync(true);

            // Act
            var result = await _taskService.AddTaskAsync(taskDto);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AddTaskAsync_ShouldReturnFalse_WhenExceptionOccurs()
        {
            // Arrange
            var taskDto = new CreateTaskDTO { Name = "Test Task" };
            _taskRepositoryMock.Setup(r => r.AddTaskAsync(It.IsAny<TaskModel>())).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _taskService.AddTaskAsync(taskDto);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteTaskAsync_ShouldReturnTrue_WhenTaskExists()
        {
            // Arrange
            var taskModel = new TaskModel { Id = 1, Name = "Test Task" };
            _taskRepositoryMock.Setup(r => r.GetTaskByIdAsync(1)).ReturnsAsync(taskModel);
            _taskRepositoryMock.Setup(r => r.DeleteTaskAsync(taskModel)).ReturnsAsync(true);

            // Act
            var result = await _taskService.DeleteTaskAsync(1);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteTaskAsync_ShouldReturnFalse_WhenTaskDoesNotExist()
        {
            // Arrange
            _taskRepositoryMock.Setup(r => r.GetTaskByIdAsync(1)).ReturnsAsync((TaskModel)null);

            // Act
            var result = await _taskService.DeleteTaskAsync(1);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetAllTasksAsync_ShouldReturnMappedTasks_WhenTasksExist()
        {
            // Arrange
            var taskModels = new List<TaskModel>
            {
                new TaskModel { Id = 1, Name = "Task 1", Description = "Test task" }
            };
            var taskDtos = new List<GetTaskDTO>
            {
                new GetTaskDTO { Id = 1, Name = "Task 1", Description = "Test task" }
            };

            _taskRepositoryMock.Setup(r => r.GetAllTasksAsync()).ReturnsAsync(taskModels);
            _mapperMock.Setup(m => m.Map<IEnumerable<GetTaskDTO>>(taskModels)).Returns(taskDtos);

            // Act
            var result = await _taskService.GetAllTasksAsync();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(taskDtos[0].Name, result.First().Name);
        }

        [Test]
        public async Task GetTaskByIdAsync_ShouldReturnTask_WhenTaskExists()
        {
            // Arrange
            var taskModel = new TaskModel { Id = 1, Name = "Test Task" };
            _taskRepositoryMock.Setup(r => r.GetTaskByIdAsync(1)).ReturnsAsync(taskModel);

            // Act
            var result = await _taskService.GetTaskByIdAsync(1);

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Test Task", result.Name);
        }

        [Test]
        public async Task GetTaskByIdAsync_ShouldReturnNull_WhenTaskDoesNotExist()
        {
            // Arrange
            _taskRepositoryMock.Setup(r => r.GetTaskByIdAsync(1)).ReturnsAsync((TaskModel)null);

            // Act
            var result = await _taskService.GetTaskByIdAsync(1);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateTaskAsync_ShouldReturnTrue_WhenTaskIsUpdated()
        {
            // Arrange
            var taskModel = new TaskModel { Id = 1, Name = "Test Task" };
            var updateDto = new UpdateTaskDTO { Name = "Updated Task" };
            _taskRepositoryMock.Setup(r => r.GetTaskByIdAsync(1)).ReturnsAsync(taskModel);

            _mapperMock.Setup(m => m.Map(updateDto, taskModel));

            _taskRepositoryMock.Setup(r => r.UpdateTaskAsync(taskModel)).ReturnsAsync(true);

            // Act
            var result = await _taskService.UpdateTaskAsync(1, updateDto);

            // Assert
            Assert.IsTrue(result);
        }


        [Test]
        public async Task GetTasksForUserIdAsync_ShouldReturnTasks_WhenTasksExistForUser()
        {
            // Arrange
            var taskModels = new List<TaskModel>
            {
                new TaskModel { Id = 1, Name = "Task 1", CreatedByUserId = "user1" }
            };
            var taskDtos = new List<GetTaskDTO>
            {
                new GetTaskDTO { Id = 1, Name = "Task 1", CreatedByUser = new UserDTO{Id = "user1", Name = "admin", SurName = "" } }
            };

            _taskRepositoryMock.Setup(r => r.GetTasksForUserId("user1")).ReturnsAsync(taskModels);
            _mapperMock.Setup(m => m.Map<IEnumerable<GetTaskDTO>>(taskModels)).Returns(taskDtos);

            // Act
            var result = await _taskService.GetTasksForUserIdAsync("user1");

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Task 1", result.First().Name);
        }

        [Test]
        public async Task GetTasksForUserIdAsync_ShouldReturnEmptyList_WhenNoTasksExistForUser()
        {
            // Arrange
            _taskRepositoryMock.Setup(r => r.GetTasksForUserId("user1")).ReturnsAsync(new List<TaskModel>());

            // Act
            var result = await _taskService.GetTasksForUserIdAsync("user1");

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
