using EducationPortal.BLL.Interfaces;
using EducationPortal.DAL.Entities;
using EducationPortal.Web.Models;
using EducationPortal.Web.Models.UserModel;
using EducationPortal.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userServ;
        private readonly UserManager<User> _userManager;
        public UserController(UserManager<User> userManager, IUserService userServ)
        {
            _userManager = userManager;
            _userServ = userServ;
        }
        

        public async Task<IActionResult> MyCourses(int page = 1)
        {
            int pageSize = 6;

            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var coursesDTO = await _userServ.GetUserCourses(userId, page, pageSize);
            var list = coursesDTO.Items.Select(x => new CourseVM()
            {
                Id = x.Id,
                NameOfCourse = x.NameOfCourse,
                Description = x.Description,
                UserHas = true,
            });

            var viewCourse = new CourseViewModel()
            {
                Courses = list,
                PageViewModel = new PageViewModel(coursesDTO.Count, page, pageSize)
            };

            return View(viewCourse);
        }

        public async Task<IActionResult> PassMaterial(int materialId, int courseId)
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            await _userServ.PassTheMaterial(userId, materialId);

            return RedirectToAction("Details", "Course", new { id = courseId });
        }
        
        public async Task<IActionResult> UserProfile()
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var user = await _userServ.GetUser(userId);

            var userView = new UserProfileModel()
            {
                Email = User.Identity.Name,
                FisrtName = user.FirstName,
                LastName = user.LastName,
                Skills = await _userServ.GetSkillDictionary(userId),
                Materials = await _userServ.GetUserMaterials(userId),
                PassedCourses = await _userServ.GetAllCompletedCourse(userId),
                InProgressCourse = await _userServ.GetAllUnCompleteCourse(userId),
            };

            return View(userView);
        }

        //public async Task<IActionResult> MyCourseDetail(int id)
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> EnterToCourse(int id)
        {
            //need validation
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            await _userServ.EnterToCourse(userId, id);

            return RedirectToAction("Details", "Course", new { id = id });
        }
    }
}
