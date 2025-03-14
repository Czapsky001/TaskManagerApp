using AutoMapper;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.SubTasks;
using TaskManagerApp.Model.Dto.Tasks;

namespace TaskManagerApp.AutoMapper;


public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateTaskDTO, TaskModel>();
        CreateMap<UpdateTaskDTO, TaskModel>();
        CreateMap<TaskModel, CreateTaskDTO>();
        CreateMap<TaskModel, UpdateTaskDTO>();
        CreateMap<GetTaskDTO, TaskModel>();
        CreateMap<ApplicationUser, UserDTO>();
        CreateMap<TaskModel, GetTaskDTO>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser))
            .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(s => s.SubTasks)); // Mapping users and subtasks for tasks
        CreateMap<SubTask, SubTaskDTO>();
        CreateMap<SubTask, CreateSubTaskDTO>();
        CreateMap<CreateSubTaskDTO, SubTask>();
        CreateMap<UpdateSubTaskDTO, SubTask>();
        CreateMap<SubTask, GetSubTaskDTO>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser))
            .ForMember(dest => dest.ParentTask, opt => opt.MapFrom(src => src.ParentTask));


    }
}
