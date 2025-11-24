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
            var taskToAdd = _mapper.Map<TaskModel>(task);
            return await _taskRepository.AddTaskAsync(taskToAdd);
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
            var itemToDelete = await _taskRepository.GetTaskByIdAsync(id);
            if(itemToDelete == null)
            {
                _logger.LogError($"Task with id - {id} does not exist");
                return false;
            }
            return await _taskRepository.DeleteTaskAsync(itemToDelete);
    }

    public async Task<IEnumerable<GetTaskDTO>> GetAllTasksAsync()
    {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return _mapper.Map<IEnumerable<GetTaskDTO>>(tasks);
    }


    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
            return await _taskRepository.GetTaskByIdAsync(id);
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
            var tasks = await _taskRepository.GetTasksForUserId(userId);
            return _mapper.Map<IEnumerable<GetTaskDTO>>(tasks);
    }

}
