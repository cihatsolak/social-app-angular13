namespace ServerApp.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(p => p.IsProfile).Name))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<User, UserForDetailDto>()
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => GetProfileImageUrl(src)))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Where(p => !p.IsProfile)));

            CreateMap<Image, ImageForDetailDto>();
            CreateMap<UserForUpdateDto, User>();

            CreateMap<MessageForCreateDto, Message>().ReverseMap();
        }

        private string GetProfileImageUrl(User user)
        {
            if (user.Images is null || !user.Images.Any())
            {
                return null;
            }

            return user.Images.FirstOrDefault(p => p.IsProfile).Name;
        }
    }
}
