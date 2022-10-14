using AutoMapper;
using EducationPortal.BLL.DTO;
using EducationPortal.Web.Models;

namespace EducationPortal.Web.Mapper
{
    public class AutoMapperData : Profile
    {
        public AutoMapperData()
        {
            CreateMap<CourseDTO, CourseVM>().ReverseMap();
        }
    }
}
