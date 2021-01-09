using DotnetCoreNLayer.API.DTO.Error;
using DotnetCoreNLayer.Core.Models;
using DotnetCoreNLayer.Core.Services;
using DotnetCoreNLayer.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.Filters
{
    public class ProductNotFoundFilter : ActionFilterAttribute
    {
        private readonly IProductService _productService;
        public ProductNotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // parameterValue is value of parameter which is passed to the action. For action: GetById(long id), it will be value of id (Ex. 5)
            // For action: Update(UpdateProductDto updateProductDto), it will be value(object) of updateProductDto class
            var parameterValue = context.ActionArguments.Values.FirstOrDefault();

            // Get properties of parameterValue to see if it is Id, or an object
            Type dataType = parameterValue.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(dataType.GetProperties());

            // If parameterValue is an object, first find ProductId (for foreign key), if not found then find Id column in that object and get its value. Otherwise Id will be parameterValue
            long Id = props.Count > 0
                ? props.Any(p => p.Name == "ProductId")
                ? (long)props.Where(p => p.Name == "ProductId").FirstOrDefault().GetValue(parameterValue, null)
                : (long)props.Where(p => p.Name == "Id").FirstOrDefault().GetValue(parameterValue, null)
                : (long)parameterValue;

            var product = await _productService.SingleOrDefaultAsync(x => x.Id == Id);

            if (product is not null)
            {
                //If Id found, keep searching
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto
                {
                    Status = 404
                };
                errorDto.Errors.Add($"Cannot find the product with Id: {Id} ");

                context.Result = new NotFoundObjectResult(errorDto);
            }
        }
    }
}
