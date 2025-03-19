using AutoMapper;
using Marvin.JsonPatch;
using System.Threading.Tasks;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.SubTasks;
using TaskManagerApp.Model.Dto.Tasks;
using TaskManagerApp.Repository.Task;

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

    public async Task<bool> AddTaskAsync(CreateTaskDTO task)
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
            if(itemToDelete == null)
            {
                _logger.LogError($"Task with id - {id} does not exist");
                return false;
            }
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
            return _mapper.Map<IEnumerable<GetTaskDTO>>(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return Enumerable.Empty<GetTaskDTO>();
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
    public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDTO updateTaskDto)
    {
        var task = await _taskRepository.GetTaskByIdAsync(id);
        if (task == null)
            return false;

        _mapper.Map(updateTaskDto, task);

        return await _taskRepository.UpdateTaskAsync(task);
    }

    public async Task<IEnumerable<GetTaskDTO>> GetTasksForUserIdAsync(string userId)
    {
        try
        {
            var tasks = await _taskRepository.GetTasksForUserId(userId);
            return _mapper.Map<IEnumerable<GetTaskDTO>>(tasks);
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<GetTaskDTO>();
        }
    }

}
