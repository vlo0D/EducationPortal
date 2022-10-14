using EducationPortal.BLL.DTO;
using FluentValidation;

namespace EducationPortal.Web.Validators.Materials
{
    public class VideoValidator : AbstractValidator<VideoMaterialDTO>
    {
        public VideoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Duration).NotEmpty();
            RuleFor(x => x.Quality).NotEmpty().Length(2, 12);    
        }
    }
}