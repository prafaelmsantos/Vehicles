namespace Vehicles.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Vehicle, VehicleDTO>().ReverseMap();

            CreateMap<VehicleImage, VehicleImageDTO>().ReverseMap();

            CreateMap<Mark, MarkDTO>().ReverseMap();

            CreateMap<Model, ModelDTO>().ReverseMap();
        }
    }
}
