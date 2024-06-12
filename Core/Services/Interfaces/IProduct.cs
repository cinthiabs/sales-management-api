using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IProduct
    {
        Task<Products> CreateProduct(Products product);
        Task<Products> UpdateProduct(Products product);
        Task<Products> DeleteProduct(int id);
        Task<Products> GetProducts();
        Task<Products> GetByIdProduct(int id);
    }
}
