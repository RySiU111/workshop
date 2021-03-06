using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;
using Workshop.API.Interfaces;

namespace Workshop.API.Data.Repositories
{
    public class CarServiceRepository : ICarServiceRepository
    {
        private readonly WorkshopContext _context;

        public CarServiceRepository(WorkshopContext context)
        {
            _context = context;
        }

        public void AddServiceRequest(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Add(serviceRequest);
        }

        public void EditServiceRequest(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Update(serviceRequest);
        }

        public async Task<ServiceRequest> GetServiceRequest(int id)
        { 
            var serviceRequest = await _context.ServiceRequests
                .Include(sr => sr.Customer)
                .Where(sr => sr.IsActive)
                .FirstOrDefaultAsync(sr => sr.Id == id);

            return serviceRequest;
        }

        public async Task<List<ServiceRequest>> GetServiceRequests()
        {
            var serviceRequests = await _context.ServiceRequests
                .Include(sr => sr.Customer)
                .Where(sr => sr.IsActive)
                .ToListAsync();

            return serviceRequests;
        }

        public async Task<Customer> FindCustomer(Customer c2)
        {
            return await _context.Customers.FirstOrDefaultAsync(c1 => 
                c1.Name.ToLower() == c2.Name.ToLower() && c1.Surname.ToLower() == c2.Surname.ToLower() &&
                c1.PhoneNumber.ToLower() == c2.PhoneNumber.ToLower() && c1.Email.ToLower() == c2.Email.ToLower());
        }

        public void DeleteServiceRequest(ServiceRequest serviceRequest)
        {
            serviceRequest.IsActive = false;
            _context.ServiceRequests.Update(serviceRequest);
        }

        public void AcceptServiceRequest(int id)
        {
            var serviceRequest = _context.ServiceRequests
                .FirstOrDefault(s => s.Id == id);

            if(serviceRequest != null)
            {
                serviceRequest.State = ServiceRequestState.Accepted;
                _context.ServiceRequests.Update(serviceRequest);
            }
        }

        public async Task<List<ServiceRequest>> GetServiceRequestsByState(ServiceRequestState state)
        {
            var serviceRequests = await _context.ServiceRequests
                .Where(sr => sr.State == state && sr.IsActive == true)
                .Include(sr => sr.Customer)
                .ToListAsync();

            return serviceRequests;
        }
    }
}