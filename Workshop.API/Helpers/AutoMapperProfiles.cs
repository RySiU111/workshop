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
            CreateMap<CustomerDto, Customer>();
            
            CreateMap<ServiceRequest, ServiceRequestDto>();
            CreateMap<KanbanTask, KanbanTaskDto>();
            
            CreateMap<KanbanTask, KanbanTaskDetailsDto>();
            CreateMap<KanbanTaskDetailsDto, KanbanTask>();

            CreateMap<KanbanTask, KanbanTaskBasketDto>();
            CreateMap<KanbanTaskBasketDto, KanbanTask>();

            CreateMap<KanbanTask, KanbanTaskHistoryDto>();
            CreateMap<KanbanTaskHistoryDto, KanbanTask>();
            
            CreateMap<Subtask, SubtaskDto>();
            CreateMap<SubtaskDto, Subtask>();

            CreateMap<BasketItem, BasketItemDto>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<BasketItem, BasketItemKanbanTaskDto>();
            CreateMap<BasketItemKanbanTaskDto, BasketItem>();

            CreateMap<KanbanComment, KanbanCommentDto>()
                .ForMember(k => k.UserName, a => a.MapFrom(s => s.User.UserName));
            CreateMap<KanbanCommentDto, KanbanComment>();
        }
    }
}