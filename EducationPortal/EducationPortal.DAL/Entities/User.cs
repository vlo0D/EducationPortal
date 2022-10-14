using Microsoft.AspNetCore.Identity;

namespace EducationPortal.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Skill> Skills { get; set; }

        public List<Course> Courses { get; set; }

        public List<Material> Materials { get; set; }
    }
}