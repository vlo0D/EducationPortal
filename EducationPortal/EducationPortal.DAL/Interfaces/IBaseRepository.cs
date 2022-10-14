using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DAL.Interfaces
{
    public interface IBaseRepository <T> where T : class
    {
        Task<List<T>> GetAll();

        Task<T> Get(int id);

        Task Create(T item);

        Task Update(int id, T updItem);

        Task Delete(int id);
    }
}
