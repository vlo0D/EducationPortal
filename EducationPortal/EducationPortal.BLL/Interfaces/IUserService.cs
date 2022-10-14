using EducationPortal.BLL.DTO;
using EducationPortal.BLL.DTO.PagedList;

namespace EducationPortal.BLL.Interfaces
{
    public interface IUserService
    {
        Task<PagedListDTO<CourseDTO>> GetUserCourses(int userID, int page, int pageSize = 6);

        Task <List<MaterialDTO>> GetUserMaterials(int userID);

        Task<List<SkillDTO>> GetUserSkills(int userID);

        Task EnterToCourse(int userID, int courseId);

        Task<UserDTO> GetUser(int userId);

        Task PassTheMaterial(int userID, int materialId);

        Task<List<CourseDTO>> GetAllCompletedCourse(int userID);

        Task<List<CourseDTO>> GetAllUnCompleteCourse(int userID);

        Task<Dictionary<string, int>> GetSkillDictionary(int userId);

        //Task<List<MaterialDTO>> GetUnpassedMaterials(int userId);

        //Task<bool> HasCourse(int userId, int courseId);
    }
}