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
        }
    }
}