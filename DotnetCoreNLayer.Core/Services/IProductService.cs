using DotnetCoreNLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        // Methods for Product entity
        Task<Product> GetWithCategoryByIdAsync(int productId);        
    }
}
