using DDD.Application.DTOs.User;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DDD.API.Providers
{
    public class TokenBuilder
    {

        private readonly IConfiguration _configuration;

        public string SecretKey { get; set; } = string.Empty;

        public TokenBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
            SecretKey = _configuration["AppSettings:SecretKey"];
        }

        //public static string GenerateAccessToken(UserBaseDto user)
        //{
        //    var handler = new JwtSecurityTokenHandler(); 
        //    var key = Encoding.ASCII.GetBytes();
        //}
    }
}
