using DDD.Application.Common;

namespace DDD.Application.DTOs.User
{
    public class UserDto : BaseDomainEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole RoleId { get; set; }
    }
}
