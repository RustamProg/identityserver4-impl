using System.Threading.Tasks;
using IdentityServer4_implementation.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer4_implementation.Services
{
    public class UserRepository: IUserRepository
    {
        private readonly SqlDbContext _context;

        public UserRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<User> FindAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            return user;
        }

        public async Task<User> FindAsyncById(long userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }
    }
}