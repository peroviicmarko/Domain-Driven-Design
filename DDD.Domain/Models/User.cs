using DDD.Application.Common;

namespace DDD.Domain.Models
{
    public class User : BaseDomainEntity
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsVerified { get; set; } = false;
        public UserRole RoleId { get; set; }
    }
}
