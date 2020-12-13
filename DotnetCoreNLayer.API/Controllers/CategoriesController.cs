using AutoMapper;
using DotnetCoreNLayer.API.DTO.Category;
using DotnetCoreNLayer.API.Filters;
using DotnetCoreNLayer.Core.Models;
using DotnetCoreNLayer.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        [ServiceFilter(typeof(CategoryNotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [ServiceFilter(typeof(CategoryNotFoundFilter))]
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProductsById(long id)
        {
            var category = await _categoryService.GetWithProductsByIdAsync(id);

            return Ok(_mapper.Map<CategoryWithProductDto>(category));
        }

        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var newCategory = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));

            return Created(string.Empty, _mapper.Map<CategoryDto>(newCategory));
        }

        [ValidationFilter]
        [ServiceFilter(typeof(CategoryNotFoundFilter))]
        [HttpPut]
        public IActionResult Update(UpdateCategoryDto updateCategoryDto)
        {
           _categoryService.Update(_mapper.Map<Category>(updateCategoryDto));

            return NoContent();
        }

        [ServiceFilter(typeof(CategoryNotFoundFilter))]
        [HttpDelete]
        public IActionResult Remove(long id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);

            return NoContent();
        }
    }
}
