using EducationPortal.DAL.DataContext;
using EducationPortal.DAL.Entities;
using EducationPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DAL.Repositories
{
    public class SkillRepository :IBaseRepository<Skill>
    {
        private PortalContext _db;

        public SkillRepository(PortalContext context)
        {
            _db = context;
        }

        public async Task Create(Skill skill)
        {
            await _db.Skills.AddAsync(skill);
        }

        public async Task Delete(int id)
        {
            Skill skill = await _db.Skills.FindAsync(id);
            if (skill != null)
            {
                _db.Skills.Remove(skill);
            }
        }

        public async Task<Skill> Get(int id)
        {
            return await _db.Skills.FindAsync(id);
        }

        public async Task<List<Skill>> GetAll()
        {
            return await _db.Skills.ToListAsync();
        }

        public async Task Update(int id, Skill updatedskill)
        {
            var objectFound = await _db.Skills.FindAsync(id);
            if (objectFound != null)
            {
                _db.Entry(objectFound).CurrentValues.SetValues(updatedskill);
            }
        }
    }
}
