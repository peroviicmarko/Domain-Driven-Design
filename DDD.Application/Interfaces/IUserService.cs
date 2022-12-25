using DDD.Application.DTOs.User;
using DDD.Domain.Interfaces;

namespace DDD.Application.Interfaces
{
    public interface IUserService : IGenericRepository<UserDto>
    {
    }
}
