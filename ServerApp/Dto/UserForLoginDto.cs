namespace ServerApp.Dto
{
    public class UserForLoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
