using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.API.Data;
using Workshop.API.Entities;

namespace Workshop.API.Interfaces
{
    public interface IClientAppRepository
    {
        Task<ComponentConfig> GetComponentConfig(string componentName);
        void AddComponentConfig(ComponentConfig componentConfig);
        void RemoveComponentConfig(ComponentConfig componentConfig);
        void EditComponentConfig(ComponentConfig componentConfig);
    }
}