using AutoMapper.Internal.Mappers;
using Npgsql;

namespace Backend.src.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OderItems { get; set; } = null!;

        static DatabaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.HasPostgresEnum<Paid>();
            modelBuilder.HasPostgresEnum<Rating>();
            modelBuilder
                .ApplyConfiguration<User>(new UserConfiguration())
                .ApplyConfiguration<Product>(new ProductConfiguration())
                .ApplyConfiguration<Order>(new OrderConfiguration())
                .ApplyConfiguration<Review>(new ReviewConfiguration())
                .ApplyConfiguration<OrderItem>(new OrderItemConfiguration())
                .ApplyConfiguration<Category>(new CatergoryConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(
                _config.GetConnectionString("DefaultConnection")
            );
            dataSourceBuilder.MapEnum<Rating>("rating");
            dataSourceBuilder.MapEnum<Role>("role");
            dataSourceBuilder.MapEnum<Paid>("paid");
            var dataSource = dataSourceBuilder.Build();
            optionsBuilder
                .UseNpgsql(dataSource)
                .AddInterceptors(new DbInterceptor())
                .UseSnakeCaseNamingConvention();
        }
    }
}
