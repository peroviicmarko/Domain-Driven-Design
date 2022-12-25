using DDD.Application.Common;

namespace DDD.Domain.Models
{
    public class User : BaseDomainEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole RoleId { get; set; }
    }
}
