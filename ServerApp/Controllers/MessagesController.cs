namespace ServerApp.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LastActiveActionFilter))]
    [Route("api/[controller]/[action]")]
    public class MessagesController : BaseApiController
    {
        private readonly ISocialRepository _socialRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessagesController(
            ISocialRepository socialRepository,
            IMapper mapper,
            IMessageRepository messageRepository)
        {
            _socialRepository = socialRepository;
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(MessageForCreateDto messageForCreateDto)
        {
            messageForCreateDto.SenderId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var recipientUser = await _socialRepository.GetUserByIdAsync(messageForCreateDto.RecipientId);
            if (recipientUser is null)
                return BadRequest();

            var message = _mapper.Map<Message>(messageForCreateDto);
            await _messageRepository.AddAsync(message);

            var messageDto = _mapper.Map<MessageForCreateDto>(message);
            return Ok(new ResultData<MessageForCreateDto>(1020, "Mesaj gönderildi.", messageDto));
        }
    }
}
