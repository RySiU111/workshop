using AutoMapper;
using Workshop.API.DTOs;
using Workshop.API.Entities;

namespace Workshop.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<ServiceRequest, ServiceRequestDto>();
            CreateMap<KanbanTask, KanbanTaskDto>();
            
            CreateMap<KanbanTask, KanbanTaskDetailsDto>();
            CreateMap<KanbanTaskDetailsDto, KanbanTask>();
            
            CreateMap<Subtask, SubtaskDto>();
            CreateMap<SubtaskDto, Subtask>();

            CreateMap<KanbanComment, KanbanCommentDto>()
                .ForMember(k => k.UserName, a => a.MapFrom(s => s.User.UserName));
            CreateMap<KanbanCommentDto, KanbanComment>();
        }
    }
}