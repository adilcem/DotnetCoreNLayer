﻿using DotnetCoreNLayer.Core.Models;
using DotnetCoreNLayer.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetWithProductsByIdAsync(long categoryId)
        {
            return await _appDbContext.Categories.Include(x => x.Products).SingleOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}
