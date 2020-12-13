using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.DTO.Category
{
    public class CategoryDto
    {                
        public virtual long Id { get; set; }
        [Required(ErrorMessage = "{0} field is required")]
        public string Name { get; set; }
    }
}
