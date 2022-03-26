namespace ServerApp.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly ISocialRepository _socialRepository;

        public UsersController(ISocialRepository socialRepository)
        {
            _socialRepository = socialRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _socialRepository.GetUsersAsync();
            if (!users.Any())
            {
                return NotFound(new ResultDetails(1000, "Kullanıcı bulunamadı."));
            }

            return Ok(new ResultDetails(9001, "İşlem Başarılı."));
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _socialRepository.GetUserByIdAsync(id);
            if (user is null)
            {
                return NotFound(new ResultDetails(1000, "Kullanıcı bulunamadı."));
            }

            return Ok(new ResultDetails(9001, "İşlem Başarılı."));
        }
    }
}
