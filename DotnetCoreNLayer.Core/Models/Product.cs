using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreNLayer.Core.Models
{
    public class Product : BaseClass
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public string InnerBarcode { get; set; }
        public virtual Category Category { get; set; }
    }
}
