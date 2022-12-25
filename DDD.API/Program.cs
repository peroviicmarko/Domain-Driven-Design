using DDD.API.Middleware;
using DDD.Application.Common;
using DDD.Application.Config;
using DDD.Data.Context;
using DDD.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var appSettingsSection = builder.Configuration.GetSection("AppSettings");
        var appSettings = appSettingsSection.Get<AppSettings>();

        builder.Services.Configure<AppSettings>(options => appSettingsSection.Bind(options));

        var connectionString = appSettings.ConnectionString ?? throw new InvalidOperationException(Constants.DBConnectionFailed);

        builder.Services.AddDbContext<TestDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add(typeof(ExceptionFilter));
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = "JWT";
            options.DefaultAuthenticateScheme = "JWT";
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.SecretKey))
            };
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", options =>
            {
                options.WithOrigins(appSettings.AllowedCorsDomains)
                .AllowCredentials()
                .AllowAnyHeader()
                .WithMethods(appSettings.AllowedMethods);
            });
        });

        DependencyContainer.RegisterServices(builder.Services);  

        var app = builder.Build();
          
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseHsts();
        app.UseCors("CorsPolicy");

        app.UseStaticFiles();

        app.UseAuthorization();
        app.UseAuthentication();

        app.UseRouting();
        app.UsePathBase(new PathString("/api"));
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}