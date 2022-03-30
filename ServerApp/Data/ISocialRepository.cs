namespace ServerApp.Data
{
    public interface ISocialRepository : IRepository<User>
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync(UserQueryParams userQueryParams);
    }
}