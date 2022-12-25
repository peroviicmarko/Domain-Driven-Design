using DDD.Application.Interfaces;
using DDD.Application.Profiles;
using DDD.Application.Services;
using DDD.Data.Repository;
using DDD.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            #region Application Layer
            services.AddScoped<IUserService, UserService>();
            #endregion


            #region Data Layer
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            services.AddAutoMapper(typeof(MappingProfiles));
        }
    }
}
