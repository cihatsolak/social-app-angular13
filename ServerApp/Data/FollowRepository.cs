namespace ServerApp.Data
{
    public class FollowRepository :  Repository<UserToUser>, IFollowRepository
    {
        private readonly SocialDbContext _context;

        public FollowRepository(SocialDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsAlreadyFollowAsync(int followerId, int userId)
        {
            return await _context.UserToUsers.AnyAsync(p => p.UserId.Equals(userId) && p.FollowerId.Equals(followerId));
        }
    }
}
