using DDD.Data.Context;
using DDD.Domain.Interfaces;
using DDD.Domain.Models;

namespace DDD.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TestDbContext context) : base(context)
        {
        }
    }
}
