using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.DTO.Category
{
    public class UpdateCategoryDto : CategoryDto
    {
        // Since default long vaiue is 0, no need for [Required]
        [Range(1, long.MaxValue, ErrorMessage = "{0} field is required must be greater than 0")]
        public override long Id { get; set; }
    }
}
