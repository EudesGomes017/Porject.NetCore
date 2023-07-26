using AutoMapper;
using Hvex.Domain.Dto;
using Hvex.Domain.Entity;

namespace Hvex.Domain.Helpers {
    public class ApiHvexProfile : Profile{
        public ApiHvexProfile() {
            //quando receber usuario transforma em UserDto qunado ele recebe UserDto ele transforma em User
            //vamos entender melhor  no services
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, AuthDto>().ReverseMap();
            CreateMap<Transformer, TransformerDto>().ReverseMap();
            CreateMap<Test, TestDto>().ReverseMap();
            CreateMap<Report, ReportDto>().ReverseMap();

        }
    }
}
