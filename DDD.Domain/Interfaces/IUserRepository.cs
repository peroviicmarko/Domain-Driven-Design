using DDD.Domain.Models;

namespace DDD.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> IsUsernameTakenAsync(string username);
        Task<bool> IsEmailTakenAsync(string email);
    }
}
