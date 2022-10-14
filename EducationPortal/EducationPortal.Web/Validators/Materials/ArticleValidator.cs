using EducationPortal.BLL.DTO;
using FluentValidation;

namespace EducationPortal.Web.Validators.Materials
{
    public class ArticleValidator : AbstractValidator<ArticleMaterialDTO>
    {
        public ArticleValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.DateOfPublish).NotEmpty();
            RuleFor(x => x.Resource).NotEmpty();
        }
    }
}
