using EducationPortal.BLL.DTO;

namespace EducationPortal.Web.Models
{
    public class CourseVM
    {
        public int Id { get; set; }

        public string NameOfCourse { get; set; }

        public string Description { get; set; }

        public List<MaterialDTO> Materials { get; set; }

        public List<SkillDTO> Skills { get; set; }

        public bool? UserHas { get; set; } 

        public DateTime Created { get; set; }

        public string UserNameCreate { get; set; }
    }
}
