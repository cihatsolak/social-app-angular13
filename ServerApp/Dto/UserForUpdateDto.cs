namespace ServerApp.Dto
{
    public class UserForUpdateDto
    {
        [Required]
        public string Introduction { get; set; }

        [Required]
        public string Hobbies { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
