using AutoMapper;
using DotnetCoreNLayer.API.DTO.Category;
using DotnetCoreNLayer.API.DTO.Person;
using DotnetCoreNLayer.API.DTO.Product;
using DotnetCoreNLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //Category Entity
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryWithProductDto>();
            CreateMap<CategoryWithProductDto, Category>();
            
            //Product Entity
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, UpdateProductDto>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<ProductWithCategoryDto, Product>();

            //Person Entity
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();
        }
    }
}
