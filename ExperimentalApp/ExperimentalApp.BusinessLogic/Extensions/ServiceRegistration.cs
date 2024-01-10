using ExperimentalApp.BusinessLogic.Interfaces;
using ExperimentalApp.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExperimentalApp.BusinessLogic.Extensions
{
    /// <summary>
    /// Represents a service registration class for business logic.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Registers business logic services.
        /// </summary>
        /// <param name="services">Represent a collection of service</param>
        /// <returns>Registered services</returns>
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<ITweetService, TweetService>();
            services.AddScoped<IFollowersService, FollowersService>();

            return services;
        }
    }
}
