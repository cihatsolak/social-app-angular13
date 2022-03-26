namespace ServerApp.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(p => p.IsProfile)))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<User, UserForDetailDto>()
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.Images.First(p => p.IsProfile).Name))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Where(p => !p.IsProfile)));

            CreateMap<Image, ImageForDetailDto>();
            CreateMap<UserForUpdateDto, User>();
        }
    }
}
