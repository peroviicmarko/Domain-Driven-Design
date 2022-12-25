using DDD.Application.Common;
using DDD.Application.DTOs.User.JWT;

namespace DDD.Application.DTOs.User.Response
{
    public class AccountCreatedResponse
    {
        public UserBaseDto User { get; set; }
        public Tokens Tokens { get; set; }

        public AccountCreatedResponse(UserBaseDto user, Tokens tokens)
        {
            User = user;
            Tokens = tokens;
        }
    }
}
