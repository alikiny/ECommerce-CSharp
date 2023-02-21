using Backend.src.Data;

namespace Backend.src
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Add the database context to the dependency injection container
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

            // Other services configuration
            // ...
        }
    }
}