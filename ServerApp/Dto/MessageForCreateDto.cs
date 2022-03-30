namespace ServerApp.Dto
{
    public class MessageForCreateDto
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
