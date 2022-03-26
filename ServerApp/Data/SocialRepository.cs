namespace ServerApp.Data
{
    public class SocialRepository : ISocialRepository
    {
        private readonly SocialDbContext _context;

        public SocialRepository(SocialDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.Include(p => p.Images).ToListAsync();
        }
    }
}
