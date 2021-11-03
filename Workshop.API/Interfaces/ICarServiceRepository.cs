using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.API.Entities;

namespace Workshop.API.Interfaces
{
    public interface ICarServiceRepository
    {
        Task<ServiceRequest> GetServiceRequest(int id);
        Task<List<ServiceRequest>> GetServiceRequests();
        void AddServiceRequest(ServiceRequest serviceRequest);
        void EditServiceRequest(ServiceRequest serviceRequest);
        Task<Customer> FindCustomer(Customer customer);
        void DeleteServiceRequest(ServiceRequest serviceRequest);
        void AcceptServiceRequest(int id);
    }
}