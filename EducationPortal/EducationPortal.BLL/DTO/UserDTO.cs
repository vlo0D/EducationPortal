using EducationPortal.DAL.Entities;

namespace EducationPortal.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<SkillDTO> Skills { get; set; }

        public List<CourseDTO> Courses { get; set; }

        public List<MaterialDTO> Materials { get; set; }
    }
}