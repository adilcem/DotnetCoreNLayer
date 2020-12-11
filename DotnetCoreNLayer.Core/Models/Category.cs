using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DotnetCoreNLayer.Core.Models
{
    public class Category : BaseClass
    {
        public Category()
        {
            Products = new Collection<Product>();
        }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
