using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories.Cache;
using Core.Interfaces.Services;
using Infrastructure.Services;
using Infrastructure.Repositories.Implementations;
using System.Reflection;

namespace Infrastructure.ExtensionMethods
{
    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services
                .ConfigureDatabase(configurationManager)
                .AddApplicationServices()
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMemoryCache();

            return services;
        }
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDbContext<EventApiDbContext>(options =>
                options.UseSqlServer(configurationManager.GetConnectionString(nameof(EventApiDbContext))));

            return services;
        }





        static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<EventRepository>();
            services.AddScoped<IEventRepository, CachedEventRepository>();
            services.AddScoped<IEventService, EventService>();

            services.AddScoped<InvitationRepository>();
            services.AddScoped<IInvitationRepository, CachedInvitationRepository>();
            services.AddScoped<IInvitationService, InvitationService>();


            services.AddScoped<ParticipantRepository>();
            services.AddScoped<IParticipantRepository, CachedParticipantRepository>();

            services.AddScoped<UserRepository>();
            services.AddScoped<IUserRepository, CachedUserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
