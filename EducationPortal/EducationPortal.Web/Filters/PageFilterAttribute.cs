using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace EducationPortal.Web.Filters
{
    public class PageFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var option = context.HttpContext.RequestServices.GetService<IOptions<PageOptions>>().Value;
             
            var page = (int)context.ActionArguments["page"];
            var pageSize = (int)context.ActionArguments["pageSize"];

            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1 || pageSize >= option.PageSize)
            {
                pageSize = 1000;
            }

            context.ActionArguments["page"] = page;
            context.ActionArguments["pageSize"] = pageSize;
        }
    }
}
