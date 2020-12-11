﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.DTO.Category
{
    public class CategoryDto
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
