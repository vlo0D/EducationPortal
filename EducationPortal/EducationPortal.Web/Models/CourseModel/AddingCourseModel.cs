using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducationPortal.Web.Models
{
    public class AddingCourseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<int> MaterialsId { get; set; }

        public List<int> SkillsId { get; set; }

        public List<SelectListItem> SelectMaterials { get; set; }

        public List<SelectListItem> SelectSkills { get; set; }
    }
}
