using DDD.Data.Context;
using DDD.Domain.Interfaces;
using DDD.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DDD.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TestDbContext context) : base(context)
        {
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            return user != null;
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            return user != null;
        }
    }
}
