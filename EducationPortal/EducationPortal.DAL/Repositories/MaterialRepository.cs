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
    public class MaterialRepository : IBaseRepository<Material>
    {
        private PortalContext _db;

        public MaterialRepository(PortalContext context)
        {
            _db = context;
        }

        public async Task Create(Material material)
        {
            await _db.Materials.AddAsync(material);
        }

        public async Task Delete(int id)
        {
            Material material = await _db.Materials.FindAsync(id);
            if (material != null)
            {
                _db.Materials.Remove(material);
            }
        }

        public async Task<Material> Get(int id)
        {
            return await _db.Materials.FindAsync(id);
        }

        public async Task<List<Material>> GetAll()
        {
            return await _db.Materials.ToListAsync();
        }

        public async Task Update(int id, Material updItem)
        {
            var objectFound = await _db.Materials.FindAsync(id);
            if (objectFound != null)
            {
                _db.Entry(objectFound).CurrentValues.SetValues(updItem);
            }
        }
    }
}
