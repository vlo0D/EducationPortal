using EducationPortal.BLL.DTO;
using FluentValidation;

namespace EducationPortal.Web.Validators
{
    public class SkillValidator : AbstractValidator<SkillDTO>
    {
        public SkillValidator()
        {
            RuleFor(skill => skill.Name).NotNull().Length(1, 20);
        }
    }
}
