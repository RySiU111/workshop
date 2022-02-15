using System.Linq;
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

            CreateMap<User, UserDetailsDto>();
            CreateMap<UserDetailsDto, User>();

            CreateMap<User, UserEditDto>();
            CreateMap<UserEditDto, User>();

            CreateMap<EmployeeDetailsDto, User>();
            CreateMap<User, EmployeeDetailsDto>();

            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleDto, UserRole>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<EmployeeDto, User>();
            CreateMap<User, EmployeeDto>();

            CreateMap<User, CalendarEntryUserDto>();
            CreateMap<CalendarEntryUserDto, User>();

            CreateMap<UserChangePasswordDto, User>();
            CreateMap<EmployeeEditDto, User>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            
            CreateMap<ServiceRequest, ServiceRequestDto>();
            CreateMap<KanbanTask, KanbanTaskDto>();
            
            CreateMap<KanbanTask, KanbanTaskDetailsDto>();
            CreateMap<KanbanTaskDetailsDto, KanbanTask>();

            CreateMap<KanbanTask, KanbanTaskBasketDto>();
            CreateMap<KanbanTaskBasketDto, KanbanTask>();

            CreateMap<KanbanTask, KanbanTaskHistoryDto>()
                .ForMember(k => k.PlannedWorkHoursCosts, 
                    x => x.MapFrom(k => k.Subtasks
                        .Where(s => s.IsActive)
                        .Sum(s => s.ManHour)))
                .ForMember(k => k.TotalWorkHoursCosts, 
                    x => x.MapFrom(k => k.Subtasks
                        .Where(s => s.IsActive)
                        .Sum(s => s.CalendarEntries
                            .Where(c => c.IsActive && !c.IsPlanned)
                            .Sum(c => c.Hours))));
            CreateMap<KanbanTaskHistoryDto, KanbanTask>();
            
            CreateMap<Subtask, SubtaskDto>();
            CreateMap<SubtaskDto, Subtask>();

            CreateMap<BasketItem, BasketItemDto>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<Photo, PhotoDto>();
            CreateMap<PhotoDto, Photo>();

            CreateMap<BasketItem, BasketItemKanbanTaskDto>();
            CreateMap<BasketItemKanbanTaskDto, BasketItem>();

            CreateMap<CalendarEntry, CalendarEntryDto>();
            CreateMap<CalendarEntryDto, CalendarEntry>();

            CreateMap<CalendarEntry, CalendarEntryAddDto>();
            CreateMap<CalendarEntryAddDto, CalendarEntry>();

            CreateMap<Invoice, InvoiceAddDto>();
            CreateMap<InvoiceAddDto, Invoice>();

            CreateMap<Invoice, InvoiceDetailsDto>();
            CreateMap<InvoiceDetailsDto, Invoice>();

            CreateMap<Invoice, InvoiceDto>()
                .ForMember(x => x.CustomerNameAndSurname, x => x.MapFrom(i => $"{i.Customer.Name} {i.Customer.Surname}"))
                .ForMember(x => x.UserNameAndSurname, x => x.MapFrom(i => $"{i.User.Name} {i.User.Surname}"))
                .ForMember(x => x.KanbanTaskName, x => x.MapFrom(i => i.KanbanTask.Name));
            CreateMap<InvoiceDto, Invoice>();

            CreateMap<KanbanComment, KanbanCommentDto>()
                .ForMember(k => k.UserName, a => a.MapFrom(s => s.User.UserName));
            CreateMap<KanbanCommentDto, KanbanComment>();
        }
    }
}