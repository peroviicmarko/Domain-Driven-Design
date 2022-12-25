using DDD.Application.DTOs.User;
using DDD.Domain.Interfaces;

namespace DDD.Application.Interfaces
{
    public interface IUserService : IGenericRepository<UserDto>
    {
        Task<UserBaseDto> CreateUserAsync(UserDto user);
        Task<bool> IsUsernameTakenAsync(string username);
        Task<bool> IsEmailTakenAsync(string email);
    }
}
