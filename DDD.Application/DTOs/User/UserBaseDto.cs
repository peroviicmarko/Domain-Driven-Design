using DDD.Application.Common;

namespace DDD.Application.DTOs.User
{
    public class UserBaseDto : BaseDomainEntity
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsVerified { get; set; } = false;
        public UserRole RoleId { get; set; }
    }
}
