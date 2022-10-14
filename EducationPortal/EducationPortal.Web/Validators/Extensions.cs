using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EducationPortal.Web.Validators
{
    public static class Extensions
    {
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            if (result == null || modelState == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
