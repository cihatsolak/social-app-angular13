namespace ServerApp.Data
{
    public interface IFollowRepository : IRepository<UserToUser>
    {
        Task<bool> IsAlreadyFollowAsync(int followerId, int userId);
    }
}
