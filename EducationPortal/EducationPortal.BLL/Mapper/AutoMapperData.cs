using AutoMapper;
using EducationPortal.BLL.DTO;
using EducationPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.BLL.Mapper
{
    public class AutoMapperData : Profile
    {
        public AutoMapperData()
        {
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Material, MaterialDTO>().ReverseMap();
            CreateMap<Skill, SkillDTO>().ReverseMap();
            CreateMap<ArticleMaterial, ArticleMaterialDTO>().ReverseMap();
            CreateMap<BookMaterial, BookMaterialDTO>().ReverseMap();
            CreateMap<VideoMaterial, VideoMaterialDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
