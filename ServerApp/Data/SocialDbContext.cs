namespace ServerApp.Data
{
    public class SocialDbContext : IdentityDbContext<User, Role, int>
    {
        public SocialDbContext(DbContextOptions<SocialDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
