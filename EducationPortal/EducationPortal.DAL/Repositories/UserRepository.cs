using EducationPortal.DAL.DataContext;
using EducationPortal.DAL.Entities;
using EducationPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private PortalContext _db;

        public UserRepository(PortalContext context)
        {
            _db = context;
        }

        public async Task Create(User item)
        {
            await _db.Users.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            User user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
            }
        }

        public async Task<User> Get(int id)
        {
            return await _db.Users.Include(c => c.Materials)
                .Include(c => c.Skills)
                .Include(x => x.Courses)
                    .ThenInclude(x => x.Materials)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
                List<User> list = await _db.Users.Include(c => c.Courses).Include(c => c.Materials).Include(c => c.Skills).ToListAsync();
                return list;
        }

        public async Task Update(int id, User updItem)
        {
            var objectFound = await _db.Users.FindAsync(id);
            if (objectFound != null)
            {
                _db.Entry(objectFound).CurrentValues.SetValues(updItem);
            }
        }
    }
}
