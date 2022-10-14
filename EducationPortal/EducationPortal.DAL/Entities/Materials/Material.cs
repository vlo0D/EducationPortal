using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DAL.Entities
{
    public class Material
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Course> Courses { get; set; }

        public List<User> Users { get; set; }

        public string UserNameCreate { get; set; }

        public DateTime Created { get; set; }
    }
}
