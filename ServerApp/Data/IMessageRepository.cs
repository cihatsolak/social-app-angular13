namespace ServerApp.Data
{
    public interface IMessageRepository : IRepository<Message>
    {
    }

    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(SocialDbContext context) : base(context)
        {
        }
    }
}
