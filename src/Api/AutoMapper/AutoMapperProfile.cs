using Api.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Api.AutoMapper
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