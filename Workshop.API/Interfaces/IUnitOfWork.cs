using System.Threading.Tasks;
using Workshop.API.Data.Repositories;

namespace Workshop.API.Interfaces
{
    public interface IUnitOfWork
    {
        ClientAppRepository ClientAppRepository { get; }
        Task<bool> SaveAsync();
    }
}