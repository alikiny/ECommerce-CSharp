namespace Backend.src.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /* Configure Review model */
            modelBuilder.Entity<Review>(
                entity =>
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
                    entity
                        .Property(e => e.Rating)
                        .HasAnnotation("MinValue", Rating.One)
                        .HasAnnotation("MaxValue", Rating.Five);
                }
            );

            /*  Configure Product Model*/
            modelBuilder.Entity<Product>(
                entity =>
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
                }
            );

            /* Configure Order model */
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            /* Configure OrderItem model*/
            modelBuilder.Entity<OrderItem>(
                entity =>
                {
                    entity
                        .HasOne(i => i.Order)
                        .WithMany()
                        .HasForeignKey(i => i.OrderId)
                        .OnDelete(DeleteBehavior.Cascade);
                    entity
                       .HasOne(i => i.Product)
                       .WithMany()
                       .HasForeignKey(i => i.ProductId)
                       .OnDelete(DeleteBehavior.Cascade);
                    entity
                        .HasKey(i => new { i.OrderId, i.ProductId });
                }
            );

            /* Configure Category model*/
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSnakeCaseNamingConvention();
        }
    }
}
