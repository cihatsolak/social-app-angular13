namespace ServerApp.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UsersController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = new()
            {
                UserName = userForRegisterDto.UserName,
                Email = userForRegisterDto.Email,
                Name = userForRegisterDto.Name,
                Gender = userForRegisterDto.Gender,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                LastActive = DateTime.Now.ToUniversalTime()
            };

            var identityResult = await _userManager.CreateAsync(user, userForRegisterDto.Password);
            if (identityResult.Succeeded)
                return StatusCode(201);

            return BadRequest(identityResult.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorDetails(9004, "Kullanıcı dogrulanamadı."));

            var user = await _userManager.FindByNameAsync(userForLoginDto.UserName);
            if (user is null)
            {
                return BadRequest(new { message = "username is incorret." });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    token = GenerateJwtToken(user)
                });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
