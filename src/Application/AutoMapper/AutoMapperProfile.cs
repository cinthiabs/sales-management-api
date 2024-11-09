using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Costs, CostsDto>().ReverseMap();

            CreateMap<Products, ProductsDto>().ReverseMap();

            CreateMap<Sales, SalesDto>().ReverseMap();

            CreateMap<Clients, ClientDto>().ReverseMap();

            CreateMap<UserCredentials, UserCredentialsDto>().ReverseMap();

            CreateMap<UserProfile, UserProfileDto>().ReverseMap();

            CreateMap<Login, LoginDto>().ReverseMap();

            CreateMap<ProductTotalCostDto, ProductTotalCosts>()
                 .ForMember(dest => dest.TotalProductCost, opt => opt.MapFrom(src => src.TotalProductCost))
                 .ForMember(dest => dest.ProductCost, opt => opt.MapFrom(src => src.UnitCost))
                 .ForMember(dest => dest.DateCreate, opt => opt.Ignore()) 
                 .ForMember(dest => dest.DateEdit, opt => opt.Ignore())
                 .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<ProductCostDto, ProductCost>()
                .ForMember(dest => dest.IdCost, opt => opt.MapFrom(src => src.IdCost))
                .ForMember(dest => dest.TotalProductPrice, opt => opt.MapFrom(src => src.TotalProductPrice))
                .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.TotalQuantity))
                .ForMember(dest => dest.QuantityRequired, opt => opt.MapFrom(src => src.QuantityRequired))
                .ForMember(dest => dest.IngredientCost, opt => opt.MapFrom(src => src.IngredientCost))
                .ForMember(dest => dest.DateCreate, opt => opt.Ignore()) 
                .ForMember(dest => dest.DateEdit, opt => opt.Ignore());

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