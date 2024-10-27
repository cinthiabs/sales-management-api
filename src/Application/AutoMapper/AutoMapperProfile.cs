using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Costs, CostsDto>();
            CreateMap<CostsDto, Costs>();

            CreateMap<Products, ProductsDto>();
            CreateMap<ProductsDto, Products>();

            CreateMap<Sales, SalesDto>();
            CreateMap<SalesDto, Sales>();

            CreateMap<Clients, ClientDto>();
            CreateMap<ClientDto, Clients>();

            CreateMap<UserCredentials, UserCredentialsDto>();
            CreateMap<UserCredentialsDto, UserCredentials>();

            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<UserProfileDto, UserProfile>();

            CreateMap<Login, LoginDto>();
            CreateMap<LoginDto, Login>();

            CreateMap<ZipCode, AddressDto>()
           .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.cep))
           .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.logradouro))
           .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.complemento))
           .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.unidade))
           .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.bairro))
           .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.localidade))
           .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.uf))
           .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.estado))
           .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.regiao))
           .ForMember(dest => dest.IbgeCode, opt => opt.MapFrom(src => src.ibge))
           .ForMember(dest => dest.GiaCode, opt => opt.MapFrom(src => src.gia));
        
        }
    }
}