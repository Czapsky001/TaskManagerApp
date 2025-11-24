using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.SubTasks;
using TaskManagerApp.Repository.SubTasksRepo;

namespace TaskManagerApp.Services.SubTasks;

public class SubTaskService : ISubTaskService
{
    private readonly ILogger<SubTaskService> _logger;
    private readonly ISubTaskRepo _subTaskRepo;
    private IMapper _mapper;

    public SubTaskService(ILogger<SubTaskService> logger, ISubTaskRepo subTaskRepo, IMapper mapper)
    {
        _logger = logger;
        _subTaskRepo = subTaskRepo;
        _mapper = mapper;
    }
    public async Task<bool> AddSubTaskAsync(CreateSubTaskDTO task)
    {
            var subTaskToAdd = _mapper.Map<SubTask>(task);
            await _subTaskRepo.CreateSubTaskAsync(subTaskToAdd);
            return true;
    }

    public async Task<bool> DeleteSubTaskAsync(int id)
    {
            var subTaskFromRepo = await _subTaskRepo.GetSubTaskByIdAsync(id);
            if(subTaskFromRepo == null)
            {
                _logger.LogError($"SubTask with id - {id} does not exist");
                return false;
            }
/*            var subTaskToDelete = _mapper.Map<SubTask>(subTaskFromRepo);*/
            return await _subTaskRepo.DeleteSubTaskAsync(subTaskFromRepo);
    }

    public IEnumerable<SubTaskDTO> GetAllSubTasksForParentTaskId(int id)
    {
            var subTasks = _subTaskRepo.GetAllSubTasksForParentTaskId(id);
            return _mapper.Map<IEnumerable<SubTaskDTO>>(subTasks);
    }

    public async Task<GetSubTaskDTO> GetSubTaskByIdAsync(int id)
    {
            var subTask = await _subTaskRepo.GetSubTaskByIdAsync(id);
            return _mapper.Map<GetSubTaskDTO>(subTask);
    }

    public IEnumerable<SubTaskDTO> GetSubTasksForUserId(string userId)
    {
            var subTasksForUserId = _subTaskRepo.GetSubTasksByUserId(userId);
            return _mapper.Map<IEnumerable<SubTaskDTO>>(subTasksForUserId);
    }

    public async Task<bool> UpdateSubTaskAsync(int id, UpdateSubTaskDTO updateSubTaskDto)
    {
            var subTask = await _subTaskRepo.GetSubTaskByIdAsync(id);
            if (subTask == null)
            {
                _logger.LogError($"Subtask with id - {id} does not exist");
                return false;
            }
            var subTaskToUpdate = _mapper.Map(updateSubTaskDto, subTask);
            await _subTaskRepo.UpdateSubTaskAsync(subTaskToUpdate);
            return true;
    }
}
