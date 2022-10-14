using AutoMapper;
using EducationPortal.BLL.DTO;
using EducationPortal.BLL.DTO.PagedList;
using EducationPortal.BLL.Infrastructure;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.Mapper;
using EducationPortal.DAL.DataContext;
using EducationPortal.DAL.Entities;
using EducationPortal.DAL.Entities.PagedList;
using EducationPortal.DAL.Interfaces;
using EducationPortal.DAL.Repositories;

namespace EducationPortal.BLL.Services
{
    public class CourseService : ICourseService
    {
        private IUnitOfWork _uof;
        private IMapper _mapper;

        public CourseService(PortalContext context)
        {
            _uof = new EFUnitOfWork(context);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        public async Task<CourseDTO> GetCourseById(int id, int userId)
        {
            bool userHas = false;
            Course course = await _uof.Courses.Get(id);
            var userCourses = (await _uof.Users.Get(userId)).Courses;
            if (userCourses.Contains(course))
            {
                userHas = true;
            }

            var courseDTO = _mapper.Map<CourseDTO>(course);
            courseDTO.UserHas = userHas;
            return courseDTO;
        }

        public async Task<PagedListDTO<CourseDTO>> GetAllCourses(int page, int pageSize, int userId)
        {
            var courses = await _uof.Courses.GetPagedAsync(page, pageSize);
            var userCourses = (await _uof.Users.Get(userId)).Courses;
            var userCoursesDTO = _mapper.Map<List<CourseDTO>>(userCourses);
            var coursesDTO = new PagedListDTO<CourseDTO>()
            {
                Items = _mapper.Map<List<CourseDTO>>(courses.Items),
                Count = courses.Count
            };

            foreach (var item in coursesDTO.Items)
            {
                item.UserHas = userCoursesDTO.Any(x => x.Id == item.Id);
            }

            return coursesDTO;
        }

        public async Task AddCourse(string nameOfCourse, string description, int[] materiallsId, int[] skillsID, string createdName)
        {
            if (nameOfCourse == null || description == null || materiallsId == null || skillsID == null)
            {
                throw new ValidationException("null", "");
            }

            List<Material> materials = new List<Material>();
            foreach (var materialId in materiallsId)
            {
                materials.Add(await _uof.Materials.Get(materialId));
            }

            List<Skill> skills = new List<Skill>();
            foreach (var skillId in skillsID)
            {
                skills.Add(await _uof.Skills.Get(skillId));
            }

            Course course = new Course
            {
                NameOfCourse = nameOfCourse,
                Description = description,
                Skills = skills,
                Materials = materials,
                Created = DateTime.Now,
                UserNameCreate = createdName
            };

            await _uof.Courses.Create(course);
            await _uof.Save();
        }

        public async Task AddSkillToCourse(int courseId, int skillId)
        {
            var course = await _uof.Courses.Get(courseId);
            if (course == null)
            {
                throw new ValidationException("course is null", "");
            }

            var skill = await _uof.Skills.Get(skillId);
            if (skill == null)
            {
                throw new ValidationException("skill is null", "");
            }

            if (course.Skills == null)
            {
                course.Skills = new List<Skill>();
            }

            course.Skills.Add(skill);
            await _uof.Courses.Update(course.Id, course);
            await _uof.Save();
        }

        public async Task AddMaterialToCourse(int courseId, int materialId)
        {
            var course = await _uof.Courses.Get(courseId);
            if (course == null)
            {
                throw new ValidationException("course is null", "");
            }

            var material = await _uof.Materials.Get(materialId);
            if (material == null)
            {
                throw new ValidationException("material is null", "");
            }

            if (course.Materials == null)
            {
                course.Materials = new List<Material>();
            }

            course.Materials.Add(material);
            await _uof.Courses.Update(course.Id, course);
            await _uof.Save();
        }

        public async Task<List<MaterialDTO>> GetAllMaterials(int userId, int courseId)
        {
            var courseMat = (await _uof.Courses.Get(courseId)).Materials;
            var matereialsDTO = new List<MaterialDTO>();

            foreach (var material in courseMat)
            {
                if (material is ArticleMaterial article)
                {
                    matereialsDTO.Add(_mapper.Map<ArticleMaterialDTO>(article));
                }
                else if (material is BookMaterial book)
                {
                    matereialsDTO.Add(_mapper.Map<BookMaterialDTO>(book));
                }
                else if (material is VideoMaterial video)
                {
                    matereialsDTO.Add(_mapper.Map<VideoMaterialDTO>(video));
                }
                else
                {
                    throw new Exception("Some wrong with material type");
                }
            }

            foreach (var mat in matereialsDTO)
            {
                mat.IsPassed = await IsPassedMaterial(userId, mat.Id);
            }

            return matereialsDTO;
        }

        public async Task UpdateCourse(int id, string name, string description, int[] materiallsId, int[] skillsID, string userNameCreate)
        {
            if (name == null || description == null || materiallsId == null || skillsID == null)
            {
                throw new ValidationException("null name or desc", "");
            }

            List<Material> materials = new List<Material>();
            foreach (var materialId in materiallsId)
            {
                materials.Add(await _uof.Materials.Get(materialId));
            }

            List<Skill> skills = new List<Skill>();
            foreach (var skillId in skillsID)
            {
                skills.Add(await _uof.Skills.Get(skillId));
            }

            var newCourse = new Course
            {
                Id = id,
                NameOfCourse = name,
                Description = description,
                Materials = materials,
                Skills = skills,
                UserNameCreate = userNameCreate,
                Created = DateTime.Now
            };

            await _uof.Courses.Update(id, newCourse);
            await _uof.Save();
        }

        public async Task DeleteCourse(int id)
        {
            await _uof.Courses.Delete(id);
            await _uof.Save();
        }

        public async Task<PagedListDTO<CourseDTO>> Search(int userId, string search, int page, int pageSize)
        {
            var coursesPaged = await _uof.Courses.Search(search, page, pageSize);
            var coursesDTOPaged = new PagedListDTO<CourseDTO>
            {
                Items = _mapper.Map<List<CourseDTO>>(coursesPaged.Items),
                Count = coursesPaged.Count
            };

            var userCourses = (await _uof.Users.Get(userId)).Courses;
            var userCoursesDTO = _mapper.Map<List<CourseDTO>>(userCourses);

            foreach (var item in coursesDTOPaged.Items)
            {
                item.UserHas = userCoursesDTO.Any(x => x.Id == item.Id);
            }

            return coursesDTOPaged;
        }

        private async Task<bool> IsPassedMaterial(int userId, int materialId)
        {
            var userMat = (await _uof.Users.Get(userId)).Materials;

            if (userMat.Any(x => x.Id == materialId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
