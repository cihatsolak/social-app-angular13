namespace ServerApp.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LastActiveActionFilter))]
    public class UsersController : BaseApiController
    {
        private readonly ISocialRepository _socialRepository;
        private readonly IFollowRepository _followRepository;
        private readonly IMapper _mapper;

        public UsersController(
            ISocialRepository socialRepository,
            IMapper mapper, 
            IFollowRepository followRepository)
        {
            _socialRepository = socialRepository;
            _mapper = mapper;
            _followRepository = followRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserQueryParams userQueryParams)
        {
            userQueryParams.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var users = await _socialRepository.GetUsersAsync(userQueryParams);
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

        [HttpPost("{userId:int:min(0)}")]
        public async Task<IActionResult> FollowUser(int userId)
        {
            int myUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var isAlreadyFollowed = await _followRepository.IsAlreadyFollowAsync(myUserId, userId);
            if (isAlreadyFollowed)
                return BadRequest(new Result(1002, "Zaten kullanıcıyı takip ediyorsunuz."));

            if (await _socialRepository.GetUserByIdAsync(userId) == null)
                return NotFound(new Result(1003, "Takip edilecek kullanıcı bulunamadı."));

            var follow = new UserToUser
            {
                UserId = userId,
                FollowerId = myUserId
            };

            await _followRepository.AddAsync(follow);
            return Ok(new Result(9001, "İşlem Başarılı"));
        }
    }
}
