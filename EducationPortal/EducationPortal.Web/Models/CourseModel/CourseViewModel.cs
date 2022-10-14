using EducationPortal.Web.Models.ViewModels;

namespace EducationPortal.Web.Models
{
    public class CourseViewModel
    {
        public IEnumerable<CourseVM>? Courses { get; set; }
        public PageViewModel? PageViewModel { get; set; }
    }
}
