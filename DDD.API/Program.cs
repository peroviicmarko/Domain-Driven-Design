using DDD.API.Middleware;
using DDD.Application.Common;
using DDD.Application.Config;
using DDD.Data.Context;
using DDD.IoC;
using DDD.IoC.Scheduler.Factory;
using DDD.IoC.Scheduler.Jobs;
using DDD.IoC.Scheduler.JobScheduler;
using DDD.IoC.Scheduler.Listeners;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.Text;

internal class Program
{
    public static IScheduler _scheduler;
    public static JobScheduler JobScheduler;
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddResponseCaching();

        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
        });

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

        builder.Services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionScopedJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
            options.UseJobFactory<JobFactory>(); 

            options.UseDefaultThreadPool(options =>
            {
                options.MaxConcurrency = 15;
            });

        });

        builder.Services.AddQuartzServer(options =>
        {
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });

        builder.Services.AddQuartzHostedService(options =>
        {
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });

        DependencyContainer.RegisterServices(builder.Services);
        builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
        builder.Services.AddScoped<StorageManagerJob>();

        NameValueCollection props = new()
            {
                {
                    "quartz.serializer.type", "binary"
                },
            };

        StdSchedulerFactory factory = new(props);
        _scheduler = factory.GetScheduler().Result;

        _scheduler.ListenerManager.AddTriggerListener(new TriggerListener());
        _scheduler.ListenerManager.AddJobListener(new JobListener());
        _scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());

        JobScheduler = new JobScheduler(_scheduler);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseHsts();
        app.UseCors("CorsPolicy");

        app.UseResponseCaching();
        app.UseResponseCompression();

        app.UseStaticFiles();

        app.UseAuthorization();
        app.UseAuthentication();

        app.UseRouting();
        app.UsePathBase(new PathString("/api"));
        app.Use(async (context, next) =>
        {
            RequestLogger.HttpLogger(context);
            await next.Invoke();
        });
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
        _scheduler.JobFactory = new JobFactory(serviceProvider);
        _scheduler.Start().Wait();

        app.Run();
    }
}