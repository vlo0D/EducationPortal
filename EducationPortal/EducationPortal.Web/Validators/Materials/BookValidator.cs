using EducationPortal.BLL.DTO;
using FluentValidation;

namespace EducationPortal.Web.Validators.Materials
{
    public class BookValidator : AbstractValidator<BookMaterialDTO>
    {
        public BookValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Author).NotEmpty();
            RuleFor(x => x.Year).NotEmpty().GreaterThan(1900).LessThan(2023);
            RuleFor(x => x.Pages).NotEmpty().LessThan(10000);
            RuleFor(x => x.Format).NotEmpty().Length(2, 12);
        }
    }
}
