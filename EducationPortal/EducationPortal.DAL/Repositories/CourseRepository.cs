using EducationPortal.DAL.DataContext;
using EducationPortal.DAL.Entities;
using EducationPortal.DAL.Entities.PagedList;
using EducationPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private PortalContext _db;

        public CourseRepository(PortalContext context)
        {
            _db = context;
        }

        public async Task Create(Course course)
        {
            await _db.Courses.AddAsync(course);
        }

        public async Task Delete(int id)
        {
            Course course = await _db.Courses.FindAsync(id);
            if (course != null)
            {
                _db.Courses.Remove(course);
            }
        }

        public async Task<Course> Get(int id)
        {
            return await _db.Courses.Include(x => x.Materials).Include(x => x.Skills).FirstAsync(x => x.Id == id);
        }

        public async Task<List<Course>> GetAll()
        {
            return await _db.Courses.Include(x => x.Materials).Include(x => x.Skills).ToListAsync();
        }

        public async Task<PagedList<Course>> GetPagedAsync(int page, int pageSize = 6)
        {
            return new PagedList<Course>()
            {
                Count = _db.Courses.Count(),
                Items = await _db.Courses.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }

        public async Task<PagedList<Course>> Search(string search, int page, int pageSize)
        {
            var courses = _db.Courses.Where(x => x.NameOfCourse.Contains(search));
            return new PagedList<Course>()
            {
                Items = await courses.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                Count = courses.Count()
            };
        }

        public async Task Update(int id, Course updItem)
        {
            var objectFound = await Get(id);
            if (objectFound != null)
            {
                _db.Entry(objectFound).CurrentValues.SetValues(updItem);
            }
        }
    }
}
