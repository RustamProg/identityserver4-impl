using System.Threading.Tasks;
using IdentityServer4_implementation.Models;

namespace IdentityServer4_implementation.Services
{
    public interface IUserRepository
    {
        Task<User> FindAsync(string username);
        Task<User> FindAsyncById(long userId);
    }
}