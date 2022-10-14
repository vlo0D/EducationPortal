using EducationPortal.BLL.DTO;

namespace EducationPortal.Web.Models.UserModel
{
    public class UserProfileModel
    {
        public string FisrtName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Dictionary<string, int> Skills { get; set; }

        public List<MaterialDTO> Materials { get; set; }

        public List<CourseDTO> PassedCourses { get; set; }

        public List<CourseDTO> InProgressCourse { get; set; }
    }
}
