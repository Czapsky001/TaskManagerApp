using AutoMapper;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;
using TaskManagerApp.Model.Dto.Tasks;

namespace TaskManagerApp.AutoMapper;


public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateTaskDto, TaskModel>();
        CreateMap<UpdateTaskDto, TaskModel>();
        CreateMap<TaskModel, CreateTaskDto>();
        CreateMap<TaskModel, UpdateTaskDto>();
        CreateMap<GetTaskDTO, TaskModel>();
        CreateMap<ApplicationUser, UserDTO>(); // Mapowanie użytkownika
        CreateMap<SubTask, SubTaskDTO>();

        CreateMap<TaskModel, GetTaskDTO>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser))
            .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(s => s.SubTasks));


    }
}
