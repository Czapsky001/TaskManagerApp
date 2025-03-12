using AutoMapper;
using Marvin.JsonPatch;
using System.Threading.Tasks;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.Tasks;
using TaskManagerApp.Repository;

namespace TaskManagerApp.Services.Task;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<TaskService> _logger;
    private readonly IMapper _mapper;

    public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<bool> AddTaskAsync(CreateTaskDto task)
    {
        try
        {
            var taskToAdd = _mapper.Map<TaskModel>(task);
            return await _taskRepository.AddTaskAsync(taskToAdd);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        try
        {
            var itemToDelete = await _taskRepository.GetTaskByIdAsync(id);
            return await _taskRepository.DeleteTaskAsync(itemToDelete);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    public async Task<IEnumerable<GetTaskDTO>> GetAllTasksAsync()
    {
        try
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            _logger.LogInformation("Mapping {Count} tasks to DTOs.", tasks.Count());
            return _mapper.Map<IEnumerable<GetTaskDTO>>(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving tasks.");
            return Enumerable.Empty<GetTaskDTO>(); // Zwracamy pustą kolekcję zamiast null
        }
    }


    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        try
        {
            return await _taskRepository.GetTaskByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
        }
    }


    public async Task<bool> UpdateTaskAsync(UpdateTaskDto updateTaskDto)
    {
        var task = await _taskRepository.GetTaskByIdAsync(updateTaskDto.Id);

        if (task == null)
        {
            throw new ArgumentException("Task not found");
        }

        _mapper.Map(updateTaskDto, task);

        await _taskRepository.UpdateTaskAsync(task);

        return true;
    }

}
