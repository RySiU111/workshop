using System.Threading.Tasks;
using Workshop.API.Data.Repositories;

namespace Workshop.API.Interfaces
{
    public interface IUnitOfWork
    {
        ClientAppRepository ClientAppRepository { get; }
        CarServiceRepository CarServiceRepository { get; }
        Task<bool> SaveAsync();
    }
}