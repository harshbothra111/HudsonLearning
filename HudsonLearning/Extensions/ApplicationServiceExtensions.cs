using Azure.Storage.Blobs;
using HudsonLearning.Data;
using HudsonLearning.Helpers;
using HudsonLearning.Interfaces;
using HudsonLearning.Services;
using Microsoft.EntityFrameworkCore;

namespace HudsonLearning.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddSingleton(x => new BlobServiceClient(
                config.GetValue<string>("BlobConnection")
            ));
            return services;
        }
    }
}
