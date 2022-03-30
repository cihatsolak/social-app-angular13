namespace ServerApp.Data
{
    public class SocialDbContext : IdentityDbContext<User, Role, int>
    {
        public SocialDbContext(DbContextOptions<SocialDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserToUser>()
                .HasKey(p => new { p.UserId, p.FollowerId });

            builder.Entity<UserToUser>()
                .HasOne(p => p.User)
                .WithMany(x => x.Followers)
                .HasForeignKey(d => d.FollowerId);

            builder.Entity<UserToUser>()
               .HasOne(p => p.Follower)
               .WithMany(x => x.Followings)
               .HasForeignKey(d => d.UserId);

            builder.Entity<UserToUser>().ToTable("UserToUser");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserToUser> UserToUsers { get; set; }
    }
}
