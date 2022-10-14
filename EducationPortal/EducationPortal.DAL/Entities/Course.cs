using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DAL.Entities
{
    public class Course
    {
        public int Id { get; set; }

        public string NameOfCourse { get; set; }

        public string Description { get; set; }

        public List<Material> Materials { get; set; }

        public List<Skill> Skills { get; set; }

        public List<User> Users { get; set; }

        public string UserNameCreate { get; set; }

        public DateTime Created { get; set; }
    }
}
