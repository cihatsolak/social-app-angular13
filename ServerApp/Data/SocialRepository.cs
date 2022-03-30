namespace ServerApp.Data
{
    public class SocialRepository : Repository<User>, ISocialRepository
    {
        private readonly SocialDbContext _context;

        public SocialRepository(SocialDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<User>> GetUsersAsync(UserQueryParams userQueryParams)
        {
            var userTable = _context.Users.
                 Where(p => !p.Id.Equals(userQueryParams.UserId)).Include(p => p.Images)
                .AsQueryable();

            if (userQueryParams.Followers)
            {
                var result = await GetFollows(userQueryParams.UserId, false);
                userTable = userTable.Where(p => result.Contains(p.Id));
            }

            if (userQueryParams.Followings)
            {
                var result = await GetFollows(userQueryParams.UserId, true);
                userTable = userTable.Where(p => result.Contains(p.Id));
            }

            return await userTable.ToListAsync();
        }

        private async Task<IEnumerable<int>> GetFollows(int userId, bool isFollowing)
        {
            var user = await _context.Users.Include(p => p.Followers)
                .Include(i => i.Followings)
                .FirstOrDefaultAsync(p => p.Id.Equals(userId));

            if (isFollowing)
            {
                return user.Followers.Where(p => p.FollowerId.Equals(userId)).Select(p => p.UserId);
            }
            else
            {
                return user.Followings.Where(p => p.UserId.Equals(userId)).Select(p => p.FollowerId);
            }
        }
    }
}
