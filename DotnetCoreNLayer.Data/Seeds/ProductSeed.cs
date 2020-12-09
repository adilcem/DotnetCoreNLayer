using DotnetCoreNLayer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreNLayer.Data.Seeds
{
    class ProductSeed : IEntityTypeConfiguration<Product>
    {
        private readonly int[] _ids;
        public ProductSeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Name = "Pen", Price = 12m, Stock = 50, CategoryId = _ids[0] },
                new Product { Id = 2, Name = "Pencil", Price = 20.50m, Stock = 10, CategoryId = _ids[0] },
                new Product { Id = 3, Name = "Marker", Price = 25.40m, Stock = 150, CategoryId = _ids[0] },
                new Product { Id = 4, Name = "Small Size Notebook", Price = 50.50m, Stock = 80, CategoryId = _ids[1] },
                new Product { Id = 5, Name = "Medium Size Notebook", Price = 60.40m, Stock = 80, CategoryId = _ids[1] },
                new Product { Id = 6, Name = "Big Size Notebook", Price = 70.90m, Stock = 80, CategoryId = _ids[1] }
                );
        }
    }
}
