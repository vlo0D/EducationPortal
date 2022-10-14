using AutoMapper;
using EducationPortal.BLL.DTO;
using EducationPortal.BLL.DTO.PagedList;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.Mapper;
using EducationPortal.DAL.DataContext;
using EducationPortal.DAL.Entities;
using EducationPortal.DAL.Interfaces;
using EducationPortal.DAL.Repositories;
using System.Linq;

namespace EducationPortal.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _uof;
        private IMapper _mapper;

        public UserService(PortalContext context)
        {
            _uof = new EFUnitOfWork(context);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        public async Task<UserDTO> GetUser(int userId)
        {
            User user = await _uof.Users.Get(userId);

            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<PagedListDTO<CourseDTO>> GetUserCourses(int userId, int page, int pageSize = 6)
        {
            User user = await _uof.Users.Get(userId);

            var coursesDTO = new PagedListDTO<CourseDTO>()
            {
                Items = _mapper.Map<List<CourseDTO>>(user.Courses.Skip((page - 1) * pageSize).Take(pageSize)),
                Count = user.Courses.Count
            };

            return coursesDTO;
        }

        public async Task<Dictionary<string, int>> GetSkillDictionary(int userId)
        {
            Dictionary<string, int> skillsDict = new Dictionary<string, int>();
            var completedCourses = (await GetAllCompletedCourse(userId));
            foreach (var course in completedCourses)
            {
                foreach (var skillDTO in course.Skills)
                {
                    if (skillsDict.Keys.Any(x => x == skillDTO.Name))
                    {
                        skillsDict[skillDTO.Name]++;
                    }
                    else
                    {
                        skillsDict.Add(skillDTO.Name, 1);
                    }
                }
            }

            return skillsDict;
        }

        public async Task<List<MaterialDTO>> GetUserMaterials(int userID)
        {
            var materials = (await _uof.Users.Get(userID)).Materials;

            var matereialsDTO = new List<MaterialDTO>();

            foreach (var material in materials)
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

            return matereialsDTO;
        }

        public async Task<List<SkillDTO>> GetUserSkills(int userID)
        {
            User user = await _uof.Users.Get(userID);

            var skillDTOs = _mapper.Map<List<SkillDTO>>(user.Skills);
            return skillDTOs;
        }

        public async Task EnterToCourse(int userID, int courseId)
        {
            var user = await _uof.Users.Get(userID);
            var course = await _uof.Courses.Get(courseId);
            if (user.Courses == null)
            {
                user.Courses = new List<Course>();
            }

            user.Courses.Add(course);
            await _uof.Users.Update(user.Id, user);
            await _uof.Save();
        }

        public async Task PassTheMaterial(int userId, int materialId)
        {
            var user = await _uof.Users.Get(userId);
            var material = await _uof.Materials.Get(materialId);
            if (user.Materials == null)
            {
                user.Materials = new List<Material>();
            }

            user.Materials.Add(material);

            //all courses that have materialId
            var coursesWithMat = user.Courses.Where(x => x.Materials.Any(y => y.Id == materialId));

            foreach (var course in coursesWithMat)
            {
                if (await IsCompleteCourse(userId, course.Id))
                {
                    user.Skills.AddRange(course.Skills);
                }
            }

            await _uof.Users.Update(userId, user);
            await _uof.Save();
        }

        public async Task<List<CourseDTO>> GetAllUnCompleteCourse(int userId)
        {
            List<CourseDTO> courseDTOs = new List<CourseDTO>();
            var user = await _uof.Users.Get(userId);

            foreach (var course in user.Courses)
            {
                if (!(await IsCompleteCourse(userId, course.Id)))
                {
                    var courseDTO = _mapper.Map<CourseDTO>(course);
                    courseDTO.CourseProgress = await GetCourseProgress(userId, course.Id);
                    courseDTOs.Add(courseDTO);
                }
            }

            return courseDTOs;
        }

        public async Task<List<CourseDTO>> GetAllCompletedCourse(int userId)
        {
            List<CourseDTO> courseDTOs = new List<CourseDTO>();
            var user = await _uof.Users.Get(userId);

            foreach (var course in user.Courses)
            {
                if (await IsCompleteCourse(userId, course.Id))
                {
                    var courseDTO = _mapper.Map<CourseDTO>(course);
                    courseDTO.CourseProgress = await GetCourseProgress(userId, course.Id);
                    courseDTOs.Add(courseDTO);
                }
            }

            return courseDTOs;
        }

        private async Task<bool> IsCompleteCourse(int userID, int courseId)
        {
            var userMat = (await _uof.Users.Get(userID)).Materials;
            var courseMat = (await _uof.Courses.Get(courseId)).Materials;
            if (courseMat.All(x => userMat.Contains(x)) && courseMat.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<int> GetCourseProgress(int userId, int courseId)
        {
            var userMat = (await _uof.Users.Get(userId)).Materials;
            var courseMat = (await _uof.Courses.Get(courseId)).Materials;
            if (userMat == null || userMat.Count == 0 || courseMat.Count == 0)
            {
                return 0;
            }

            var userPassed = courseMat.Where(x => userMat.Contains(x)).ToList();
            var progress = 100 * ((decimal)userPassed.Count / courseMat.Count);
            return (int)Math.Round(progress);
        }
    }
}
