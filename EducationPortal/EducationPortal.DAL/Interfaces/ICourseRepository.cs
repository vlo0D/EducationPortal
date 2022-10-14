using EducationPortal.DAL.Entities;
using EducationPortal.DAL.Entities.PagedList;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DAL.Interfaces
{
    public interface ICourseRepository :IBaseRepository<Course>
    {
        Task<PagedList<Course>> GetPagedAsync(int page, int pageSize);

        Task<PagedList<Course>> Search(string name, int page, int pageSize);
    }
}
