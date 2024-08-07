using Api.DTOs;
using AutoMapper;
using Domain.Entities;
using sales_management_api.DTOs;

namespace sales_management_api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Costs, CostsDTO>();
            CreateMap<CostsDTO, Costs>();

            CreateMap<Products, ProductsDTO>();
            CreateMap<ProductsDTO, Products>();

            CreateMap<Sales, SalesDTO>();
            CreateMap<SalesDTO, Sales>();

            CreateMap<Clients, ClientDTO>();
            CreateMap<ClientDTO, Clients>();

            CreateMap<UserCredentials, UserCredentialsDTO>();
            CreateMap<UserCredentialsDTO, UserCredentials>();

            CreateMap<UserProfile, UserProfileDTO>();
            CreateMap<UserProfileDTO, UserProfile>();
        }
    }
}