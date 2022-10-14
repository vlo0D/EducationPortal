using EducationPortal.DAL.DataContext;
using EducationPortal.DAL.Entities;
using EducationPortal.DAL.Interfaces;

namespace EducationPortal.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private PortalContext _db;
        private IBaseRepository<User> _userRepository;
        private ICourseRepository _courseRepository;
        private IBaseRepository<Material> _materialRepository;
        private IBaseRepository<Skill> _skillRepository;

        //Пересмотреть этот Конструктор и Конструктор Портал контекста
        public EFUnitOfWork(PortalContext context)
        {
            _db = context;
        }

        public IBaseRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_db);
                }

                return _userRepository;
            }
        }

        public IBaseRepository<Material> Materials
        {
            get
            {
                if (_materialRepository == null)
                {
                    _materialRepository = new MaterialRepository(_db);
                }

                return _materialRepository;
            }
        }

        public ICourseRepository Courses
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new CourseRepository(_db);
                }

                return _courseRepository;
            }
        }

        public IBaseRepository<Skill> Skills
        {
            get
            {
                if (_skillRepository == null)
                {
                    _skillRepository = new SkillRepository(_db);
                }

                return _skillRepository;
            }
        }

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
