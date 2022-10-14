using EducationPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BLL.Interfaces
{
    public interface IMaterialService
    {
        Task AddMaterial(MaterialDTO materialDTO);

        Task<List<MaterialDTO>> GetAll();

        Task<MaterialDTO> Get(int id);

        Task Update(MaterialDTO newMaterialDTO);

        Task Delete(int id);
    }
}
