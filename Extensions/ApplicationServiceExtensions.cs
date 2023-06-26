using API.Services;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, IConfiguration config
        )
        {
            services.AddDbContext<DataContext>(
                options =>
                options.UseSqlite( config.GetConnectionString("DefaultConnection" ))
            );
            // // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}