using DDD.Application.DTOs.User;
using DDD.Application.DTOs.User.Request;

namespace DDD.API.Providers
{
    public class ObjectMapper
    {
        public static UserDto CreateAccountMap(CreateAccount createAccount)
        {
            return new UserDto
            {
                Username = createAccount.Username,
                Email = createAccount.Email,
                Fullname = createAccount.Fullname,
                Password = createAccount.Password,
                Phone = createAccount.Phone
            };
        }
    }
}
