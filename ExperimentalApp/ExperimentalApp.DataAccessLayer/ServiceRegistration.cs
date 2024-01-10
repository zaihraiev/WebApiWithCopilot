using ExperimentalApp.DataAccessLayer.DataDbContext;
using ExperimentalApp.DataAccessLayer.Interfaces;
using ExperimentalApp.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExperimentalApp.DataAccessLayer
{
    /// <summary>
    /// Service registration.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Setup data base context. Configure the connection string.
        /// </summary>
        /// <param name="services">Represent a collection of service</param>
        /// <param name="configuration">Represents configuration options.</param>
        public static void AddServicesDependenciesDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DvdRentalContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DvdRentalContext")));

            services.AddScoped<ITweetRepository,TweetRepository>();
            services.AddScoped<IFollowersRepository,FollowersRepository>();
        }
    }
}
