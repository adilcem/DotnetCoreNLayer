using DotnetCoreNLayer.Core.Repositories;
using DotnetCoreNLayer.Core.UnitOfWork;
using DotnetCoreNLayer.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync(); 
        }
    }
}
