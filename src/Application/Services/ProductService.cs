﻿using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ProductService : IProduct
    {
        public Task<Products> CreateProduct(Products product)
        {
            throw new NotImplementedException();
        }

        public Task<Products> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Products> GetByIdProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Products> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Products> UpdateProduct(Products product)
        {
            throw new NotImplementedException();
        }
    }
}
