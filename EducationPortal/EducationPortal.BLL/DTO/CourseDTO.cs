using EducationPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BLL.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }

        public string NameOfCourse { get; set; }

        public string Description { get; set; }

        public List<MaterialDTO> Materials { get; set; }

        public List<SkillDTO> Skills { get; set; }

        public int? CourseProgress { get; set; }

        public bool? UserHas { get; set; }

        public string UserNameCreate { get; set; }

        public DateTime Created { get; set; }
    }
}
