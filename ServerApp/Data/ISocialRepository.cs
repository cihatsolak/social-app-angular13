namespace ServerApp.Data
{
    public interface ISocialRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
    }
}