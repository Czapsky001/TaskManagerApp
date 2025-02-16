using AutoMapper;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Repository;

namespace TaskManagerApp.Services;

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

    public Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
        try
        {
            return _taskRepository.GetAllTasksAsync();
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
        }
    }

    public Task<TaskModel> GetTaskByIdAsync(int id)
    {
        try
        {
            return _taskRepository.GetTaskByIdAsync(id);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
        }
    }


    public async Task<bool> UpdateTaskAsync(UpdateTaskDto updateTask)
    {
        try
        {
            var taskToUpdate = _mapper.Map<TaskModel>(updateTask);
            return await _taskRepository.UpdateTaskAsync(taskToUpdate);
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
