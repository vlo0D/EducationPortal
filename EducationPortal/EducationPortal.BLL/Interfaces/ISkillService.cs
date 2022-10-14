using EducationPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BLL.Interfaces
{
    public interface ISkillService
    {
        Task<List<SkillDTO>> GetAll();

        Task AddSkill(SkillDTO skillDTO);

        Task<SkillDTO> GetSkill(int id);

        Task DeleteSkill(int id);

        Task Update(SkillDTO skillDTO);
    }
}
