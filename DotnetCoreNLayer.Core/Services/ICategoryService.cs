using DotnetCoreNLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        // Methods for Category entity
        Task<Category> GetWithProductsByIdAsync(int categoryId);        
    }
}
