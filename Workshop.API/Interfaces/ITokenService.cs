using System.Threading.Tasks;
using Workshop.API.Entities;

namespace Workshop.API.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}