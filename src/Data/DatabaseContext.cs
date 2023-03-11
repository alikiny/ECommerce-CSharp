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

            /* Configure Review model */
            modelBuilder.Entity<Review>(entity =>
            {
                entity
                    .HasOne(r => r.User)
                    .WithMany()
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity
                    .HasOne(r => r.Product)
                    .WithMany()
                    .HasForeignKey(r => r.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.Rating).HasColumnType("rating");
            });

            /*  Configure Product Model*/
            modelBuilder.Entity<Product>(entity =>
            {
                entity
                    .HasOne(p => p.User)
                    .WithMany()
                    .HasForeignKey(p => p.SellerId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity
                    .HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Role).HasColumnType("role");
            });

            /* Configure Order model */
            modelBuilder.Entity<Order>(entity =>
            {
                entity
                    .HasOne(o => o.User)
                    .WithMany()
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.Status).HasColumnType("paid");
            });

            /* Configure Category model*/
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

            /* OrderItem */
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity
                    .HasOne(r => r.Order)
                    .WithMany()
                    .HasForeignKey(r => r.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity
                    .HasOne(r => r.Product)
                    .WithMany()
                    .HasForeignKey(r => r.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
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
            optionsBuilder.UseNpgsql(dataSource).UseSnakeCaseNamingConvention();
        }
    }
}
