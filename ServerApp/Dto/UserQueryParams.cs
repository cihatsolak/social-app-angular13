namespace ServerApp.Dto
{
    public class UserQueryParams
    {
        public int UserId { get; set; }
        public bool Followers { get; set; }
        public bool Followings { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 100;
        public string City { get; set; }
        public string Country { get; set; }
    }
}
