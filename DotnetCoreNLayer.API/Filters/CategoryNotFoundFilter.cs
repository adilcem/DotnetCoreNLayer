using DotnetCoreNLayer.API.DTO.Error;
using DotnetCoreNLayer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.Filters
{
    public class CategoryNotFoundFilter : ActionFilterAttribute
    {
        private readonly ICategoryService _caregoryService;
        public CategoryNotFoundFilter(ICategoryService caregoryService)
        {
            _caregoryService = caregoryService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // parameterValue is value of parameter which is passed to the action. For action: GetById(long id), it will be value of id (Ex. 5)
            // For action: Update(UpdateProductDto updateProductDto), it will be value(object) of updateProductDto class
            var parameterValue = context.ActionArguments.Values.FirstOrDefault();

            // Get properties of parameterValue to see if it is Id, or an object
            Type dataType = parameterValue.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(dataType.GetProperties());

            // If parameterValue is an object, first find CategoryId (for foreign key), if not found then find Id column in that object and get its value. Otherwise Id will be parameterValue
            long Id = props.Count > 0
                ? props.Any(p => p.Name == "CategoryId")
                ? (long)props.Where(p => p.Name == "CategoryId").FirstOrDefault().GetValue(parameterValue, null)
                : (long)props.Where(p => p.Name == "Id").FirstOrDefault().GetValue(parameterValue, null)
                : (long)parameterValue;

            var category = await _caregoryService.SingleOrDefaultAsync(x => x.Id == Id);

            if (category is not null)
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
                errorDto.Errors.Add($"Cannot find the category with Id: {Id} ");

                context.Result = new NotFoundObjectResult(errorDto);
            }
        }
    }
}