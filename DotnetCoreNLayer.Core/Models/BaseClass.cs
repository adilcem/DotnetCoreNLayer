using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.Core.Models
{
    public abstract class BaseClass
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
