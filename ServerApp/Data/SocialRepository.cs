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
                 Where(p => !p.Id.Equals(userQueryParams.UserId))
                .Include(p => p.Images)
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

            if (!string.IsNullOrWhiteSpace(userQueryParams.Gender))
            {
                userTable = userTable.Where(p => p.Gender.ToLower().Equals(userQueryParams.Gender.ToLower()));
            }

            if (userQueryParams.MinAge != 18 || userQueryParams.MaxAge != 100)
            {
                var today = DateTime.Now;
                var minDate = today.AddYears(-(userQueryParams.MaxAge + 1));
                var maxDate = today.AddYears(-userQueryParams.MinAge);

                userTable = userTable.Where(p => p.DateOfBirth >= minDate && p.DateOfBirth <= maxDate);
            }

            if (!string.IsNullOrWhiteSpace(userQueryParams.City))
            {
                userTable = userTable.Where(p => p.City.ToLower().Equals(userQueryParams.City.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(userQueryParams.Country))
            {
                userTable = userTable.Where(p => p.Country.ToLower().Equals(userQueryParams.Country.ToLower()));
            }

            if (!string.IsNullOrEmpty(userQueryParams.OrderBy))
            {
                if (userQueryParams.OrderBy.Equals("age"))
                {
                    userTable = userTable.OrderBy(p => p.DateOfBirth);
                }

                if (userQueryParams.OrderBy.Equals("createdDate"))
                {
                    userTable = userTable.OrderByDescending(p => p.CreatedDate);
                }

                if (userQueryParams.OrderBy.Equals("lastActive"))
                {
                    userTable = userTable.OrderByDescending(p => p.LastActive);
                }
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
