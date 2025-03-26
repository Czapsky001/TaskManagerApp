using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Threading.Tasks;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.SubTasks;
using TaskManagerApp.Model.Dto.Tasks;
using TaskManagerApp.Repository.SubTasksRepo;
using TaskManagerApp.Services.SubTasks;

namespace TaskManagerAppTests
{
    [TestFixture]
    public class SubTaskServiceTest
    {
        private SubTaskService _subTaskService;
        private Mock<ISubTaskRepo> _subTaskRepoMock;
        private Mock<ILogger<SubTaskService>> _loggerMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void SetUp()
        {
            _subTaskRepoMock = new Mock<ISubTaskRepo>();
            _loggerMock = new Mock<ILogger<SubTaskService>>();
            _mapperMock = new Mock<IMapper>();
            _subTaskService = new SubTaskService(_loggerMock.Object, _subTaskRepoMock.Object, _mapperMock.Object);
        }

        [Test]
        public void GetSubTasksForParentTaskId_ShouldReturnSubTasks_WhenSubTasksExist()
        {
            var parentId = 1;
            // Arrange
            var subTaskModel = new List<SubTask>
            {
                new SubTask("SubTask1", "desc", 1, "1"),
                new SubTask("SubTask2", "desc2", 2, "1"),
            };
            var subTaskDtos = new List<SubTaskDTO>
            {
                new SubTaskDTO{Id = 1, Name = "SubTask1", ParentTaskId = 1},
                new SubTaskDTO{Id = 2, Name = "SubTask2", ParentTaskId = 1},
            };
            _subTaskRepoMock.Setup(r => r.GetAllSubTasksForParentTaskId(parentId)).Returns(subTaskModel);
            _mapperMock.Setup(m => m.Map<IEnumerable<SubTaskDTO>>(subTaskModel)).Returns(subTaskDtos);
            //Act
            var result = _subTaskService.GetAllSubTasksForParentTaskId(parentId);

            //Assert
            Assert.AreEqual(subTaskDtos, result);
            CollectionAssert.AreEqual(subTaskDtos, result.ToList());
        }

        [Test]
        public void GetSubTasksForParentTaskId_ShouldReturnEmptyList_WhenExceptionIsThrown()
        {
            // Arrange
            var parentId = 1;
            _subTaskRepoMock.Setup(r => r.GetAllSubTasksForParentTaskId(parentId)).Throws(new Exception("Database error"));

            // Act
            var result = _subTaskService.GetAllSubTasksForParentTaskId(parentId);

            // Assert
            _subTaskRepoMock.Verify(r => r.GetAllSubTasksForParentTaskId(parentId), Times.Once);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetSubTaskByIdAsync_ShouldReturnEmptySubTask_WhenSubTaskDoesNotExist()
        {
            // Arrange
            var subTaskId = 1;
            _subTaskRepoMock.Setup(r => r.GetSubTaskByIdAsync(subTaskId)).Throws(new ArgumentNullException());
            // Act
            var result = await _subTaskService.GetSubTaskByIdAsync(subTaskId);
            // Assert
            Assert.AreEqual(null, result.Name);
            Assert.AreEqual(null, result.Description);
            Assert.AreEqual(null, result.CreatedByUser);
        }

        [Test]
        public async Task AddSubTaskAsync_ShouldReturnTrue_WhenSubTaskIsAdded()
        {
            // Arrange
            var createSubTaskDTO = new CreateSubTaskDTO { ParentTaskId = 1, CreatedByUserId = "1", Description = "ssdsd", Name = "new" };
            var subTask = new SubTask("name", "desc", 1, "1");
            _mapperMock.Setup(m => m.Map<SubTask>(createSubTaskDTO)).Returns(subTask);
            _subTaskRepoMock.Setup(r => r.CreateSubTaskAsync(subTask)).ReturnsAsync(true);
            // Act
            var result = await _subTaskService.AddSubTaskAsync(createSubTaskDTO);
            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AddSubTaskAsync_ShouldReturnFalse_WhenExceptionOccurs()
        {
            // Arrange
            var createSubTaskDTO = new CreateSubTaskDTO { ParentTaskId = 1, CreatedByUserId = "1", Description = "ssdsd", Name = "new" };
            var subTask = new SubTask("name", "desc", 1, "1");
            _mapperMock.Setup(m => m.Map<SubTask>(createSubTaskDTO)).Returns(subTask);
            _subTaskRepoMock.Setup(r => r.CreateSubTaskAsync(subTask)).ThrowsAsync(new Exception());
            // Act
            var result = await _subTaskService.AddSubTaskAsync(createSubTaskDTO);
            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteSubTaskAsync_SubTaskNotFound_ReturnsFalse()
        {
            // Arrange
            int subTaskId = 1;
            _subTaskRepoMock.Setup(r => r.GetSubTaskByIdAsync(subTaskId)).ReturnsAsync((SubTask?)null);

            // Act
            var result = await _subTaskService.DeleteSubTaskAsync(subTaskId);

            // Assert
            Assert.False(result);

            // Verify that DeleteSubTaskAsync was never called
            _subTaskRepoMock.Verify(r => r.DeleteSubTaskAsync(It.IsAny<SubTask>()), Times.Never);
        }


        [Test]
        public async Task DeleteSubTaskAsync_SubTaskExists_ReturnsTrue()
        {
            // Arrange
            int subTaskId = 1;
            var subTask = new SubTask("name", "desc", 1, "1");
            _subTaskRepoMock.Setup(r => r.DeleteSubTaskAsync(subTask)).ReturnsAsync(true);
            _subTaskRepoMock.Setup(r => r.GetSubTaskByIdAsync(subTaskId)).ReturnsAsync(subTask);

            // Act
            var result = await _subTaskService.DeleteSubTaskAsync(subTaskId);

            // Assert
            Assert.True(result);
        }

        [Test]
        public async Task UpdateSubTaskAsync_ShouldReturnTrue_WhenSubTaskIsUpdated()
        {
            // Arrange
            var subTask = new SubTask("SubTask", "desc", 1, "1");
            var updateDto = new UpdateSubTaskDTO {Name = "updated" };
            _mapperMock.Setup(m => m.Map(updateDto, subTask));
            _subTaskRepoMock.Setup(r => r.GetSubTaskByIdAsync(1)).ReturnsAsync(subTask);
            _subTaskRepoMock.Setup(r => r.UpdateSubTaskAsync(subTask)).ReturnsAsync(true);
            // Act
            var result = await _subTaskService.UpdateSubTaskAsync(1, updateDto);
            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetSubTasksForUserId_ShouldReturnSubTasks_WhenSubTasksExist()
        {
            // Arrange
            var userId = "1";
            var subTasks = new List<SubTask>
    {
        new SubTask("SubTask", "desc", 1, "1")
    };

            var subTaskDTOs = new List<SubTaskDTO>
    {
        new SubTaskDTO { Name = "SubTask"}
    };

            _subTaskRepoMock.Setup(r => r.GetSubTasksByUserId(userId))
                .Returns(subTasks);

            _mapperMock.Setup(m => m.Map<IEnumerable<SubTaskDTO>>(subTasks))
                .Returns(subTaskDTOs);

            // Act
            var result = _subTaskService.GetSubTasksForUserId(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("SubTask", result.First().Name);
        }

    }
}
