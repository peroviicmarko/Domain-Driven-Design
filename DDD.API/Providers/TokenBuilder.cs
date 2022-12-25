using DDD.Application.DTOs.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DDD.API.Providers
{
    public class TokenBuilder
    {

        public static string SignInKey { get; set; } = string.Empty;

        private static string GenerateJwt(Claim[] claims, DateTime expireTime)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SignInKey);

            ClaimsIdentity identity = new(claims);
            SymmetricSecurityKey securityKey = new(key);
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            SecurityTokenDescriptor descriptor = new ()
            {
                Subject = identity,
                SigningCredentials = credentials,
                Expires = expireTime
            };

            var tokenCreator = handler.CreateToken(descriptor);
            return handler.WriteToken(tokenCreator);
        }

        public static string GenerateAccessToken(UserBaseDto user)
        {
            DateTime expiresTime = DateTime.Now.AddMinutes(15);

            Claim[] claims = new Claim[] {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Username", user.Username),
                new Claim("Email", user.Email),
                new Claim("Phone", user.Phone),
                new Claim("RoleId", user.RoleId.ToString()),
                new Claim("IsVerified", user.IsVerified.ToString()),
                new Claim("Fullname", user.Fullname),
            };

            string token = GenerateJwt(claims, expiresTime);
            return token;
        }

        public static string GenerateRefreshTokenToken(UserBaseDto user)
        {
            DateTime expiresTime = DateTime.Now.AddHours(1);

            Claim[] claims = new Claim[] {
                new Claim("UserId", user.Id.ToString()),
                new Claim("RoleId", user.RoleId.ToString()),
            };

            string token = GenerateJwt(claims, expiresTime);
            return token;
        }
    }
}
