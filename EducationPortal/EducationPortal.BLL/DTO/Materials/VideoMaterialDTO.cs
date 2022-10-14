using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BLL.DTO
{
    public class VideoMaterialDTO : MaterialDTO
    {
        public DateTime Duration { get; set; }

        public string Quality { get; set; }
    }
}
