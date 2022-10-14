using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.DAL.Entities
{
    public class VideoMaterial : Material
    {
        public DateTime Duration { get; set; }

        public string Quality { get; set; }
    }
}
