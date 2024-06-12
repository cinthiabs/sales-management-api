using Core.Services.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
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
