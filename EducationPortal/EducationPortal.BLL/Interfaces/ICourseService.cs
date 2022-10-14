using EducationPortal.BLL.DTO;
using EducationPortal.BLL.DTO.PagedList;

namespace EducationPortal.BLL.Interfaces
{
    public interface ICourseService
    {
        Task<PagedListDTO<CourseDTO>> GetAllCourses(int page, int pageSize, int userId);

        Task<CourseDTO> GetCourseById(int id, int userId);

        Task AddCourse(string nameOfCourse, string description, int[] materiallsId, int[] skillsID, string createdName);

        Task AddSkillToCourse(int courseId, int skillId);

        Task AddMaterialToCourse(int courseId, int materialId);

        Task UpdateCourse(int id, string name, string description, int[] materiallsId, int[] skillsID, string userNameCreate);

        Task<List<MaterialDTO>> GetAllMaterials(int userId, int courseId);

        Task DeleteCourse(int id);

        Task<PagedListDTO<CourseDTO>> Search(int userId, string search, int page, int pageSize);
    }
}