using DotnetCoreNLayer.API.DTO.Error;
using DotnetCoreNLayer.Core.Models;
using DotnetCoreNLayer.Core.Services;
using DotnetCoreNLayer.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.Filters
{
    // This filter will be used in the methods that gets Id as a parameter
    public class ProductNotFoundFilter : ActionFilterAttribute
    {
        private readonly IProductService _productService;
        public ProductNotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var product = await _productService.GetByIdAsync(id);

            if (!(product is null))
            {
                //If Id found, keep searching
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();

                errorDto.Status = 404;
                errorDto.Errors.Add($"Cannot find the product with Id: {id} ");

                context.Result = new NotFoundObjectResult(errorDto);
            }
        }
    }
}
