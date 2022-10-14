﻿using EducationPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BLL.DTO
{
    public class SkillDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserNameCreate { get; set; }

        public DateTime Created { get; set; }
    }
}
