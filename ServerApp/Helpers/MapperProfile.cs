namespace ServerApp.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(p => p.IsProfile)))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<User, UserForDetailDto>();
            CreateMap<Image, ImageForDetailDto>();
        }
    }
}
