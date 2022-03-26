namespace ServerApp.Dto
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string Hobbies { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ImageForDetailDto Image { get; set; }
    }
}
