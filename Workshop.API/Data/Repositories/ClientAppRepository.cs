using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;
using Workshop.API.Interfaces;

namespace Workshop.API.Data.Repositories
{
    public class ClientAppRepository : IClientAppRepository
    {
        private readonly WorkshopContext _context;
        public ClientAppRepository(WorkshopContext context)
        {
            _context = context;
        }

        public void AddComponentConfig(ComponentConfig componentConfig)
        {
            _context.ComponentConfigs.Add(componentConfig);
        }

        public void RemoveComponentConfig(ComponentConfig componentConfig)
        {
            _context.ComponentConfigs.Remove(componentConfig);
        }

        public void EditComponentConfig(ComponentConfig componentConfig)
        {
            _context.ComponentConfigs.Update(componentConfig);
        }

        public async Task<ComponentConfig> GetComponentConfig(string componentName)
        {
            var configs = await _context.ComponentConfigs
                .FirstOrDefaultAsync(c => c.ComponentName == componentName);

            return configs;
        }
    }
}