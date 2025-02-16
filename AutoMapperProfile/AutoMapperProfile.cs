

using AutoMapper;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto;

namespace TaskManagerApp.AutoMapper;


public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateTaskDto, TaskModel>();
        CreateMap<UpdateTaskDto, TaskModel>();
        CreateMap<TaskModel, CreateTaskDto>();
        CreateMap<TaskModel, UpdateTaskDto>();
    }
}
