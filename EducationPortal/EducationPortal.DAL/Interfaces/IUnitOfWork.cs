using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationPortal.DAL.Entities;

namespace EducationPortal.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> Users { get; }

        IBaseRepository<Material> Materials { get; }

        ICourseRepository Courses { get; }

        IBaseRepository<Skill> Skills { get; }

        Task Save();
    }
}
