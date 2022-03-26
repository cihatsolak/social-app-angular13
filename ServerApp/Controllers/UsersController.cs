namespace ServerApp.Controllers
{
    //[Authorize]
    public class UsersController : BaseApiController
    {
        private readonly ISocialRepository _socialRepository;
        private readonly IMapper _mapper;

        public UsersController(
            ISocialRepository socialRepository, 
            IMapper mapper)
        {
            _socialRepository = socialRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _socialRepository.GetUsersAsync();
            if (!users.Any())
            {
                return NotFound(new Result(1000, "Kullanıcı bulunamadı."));
            }

            List<UserForListDto> userForListDtos = _mapper.Map<List<UserForListDto>>(users);

            return Ok(new ResultData<List<UserForListDto>>(9001, "İşlem Başarılı.", userForListDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _socialRepository.GetUserByIdAsync(id);
            if (user is null)
            {
                return NotFound(new Result(1000, "Kullanıcı bulunamadı."));
            }
            var userForDetailDto = _mapper.Map<UserForDetailDto>(user);

            return Ok(new ResultData<UserForDetailDto>(9001, "İşlem Başarılı.", userForDetailDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            _ = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId);

            if (userId != id)
                return BadRequest(new Result(1001, "Yetkisiz erişim."));

            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _socialRepository.GetUserByIdAsync(id);

            _mapper.Map(userForUpdateDto, user);

            _socialRepository.Update(user);

            return Ok(new Result(9001, "İşlem Başarılı"));
        }
    }
}
