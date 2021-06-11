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
                .FirstOrDefaultAsync(sr => sr.Id == id);

            return serviceRequest;
        }

        public async Task<List<ServiceRequest>> GetServiceRequests()
        {
            var serviceRequests = await _context.ServiceRequests
                .Include(sr => sr.Customer)
                .ToListAsync();

            return serviceRequests;
        }

        public async Task<Customer> FindCustomer(Customer c2)
        {
            return await _context.Customers.FirstOrDefaultAsync(c1 => 
                c1.Name == c2.Name && c1.Surname == c2.Surname &&
                c1.PhoneNumber == c2.PhoneNumber && c1.Email == c2.Email);
        }
    }
}