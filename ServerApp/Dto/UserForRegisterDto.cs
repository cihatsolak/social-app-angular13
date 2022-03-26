namespace ServerApp.Dto
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "Ad gerekli bir alan.")]
        [StringLength(50, MinimumLength = 10)]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Gender { get; set; }
    }
}
