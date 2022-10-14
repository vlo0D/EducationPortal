using EducationPortal.Web.Models;
using FluentValidation;


namespace EducationPortal.Web.Validators
{
    public class CourseValidator : AbstractValidator<AddingCourseModel>
    {
        public CourseValidator()
        {
            RuleFor(course =>course.Name).NotNull().Length(3,20);
            RuleFor(course => course.Description).NotNull().Length(10, 100);
            RuleFor(course => course.SkillsId).NotNull();
            RuleFor(course => course.MaterialsId).NotNull();
        }
    }
}
