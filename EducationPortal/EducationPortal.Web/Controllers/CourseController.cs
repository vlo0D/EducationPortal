using AutoMapper;
using EducationPortal.BLL.DTO;
using EducationPortal.BLL.Interfaces;
using EducationPortal.DAL.Entities;
using EducationPortal.Web.Filters;
using EducationPortal.Web.Mapper;
using EducationPortal.Web.Models;
using EducationPortal.Web.Models.ViewModels;
using EducationPortal.Web.Validators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;

namespace EducationPortal.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseServ;
        private readonly IMaterialService _materialServ;
        private readonly ISkillService _skillServ;
        private readonly IUserService _userServ;
        private readonly IValidator<AddingCourseModel> _validator;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CourseController(UserManager<User> userManager, ICourseService courseService, IMaterialService materialService, ISkillService skillService, IUserService userService, IValidator<AddingCourseModel> validator)
        {
            _userManager = userManager;
            _userServ = userService;
            _materialServ = materialService;
            _skillServ = skillService;
            _courseServ = courseService;
            _validator = validator;

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        // GET: Course
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            int pageSize = 6;
            var coursesDTO = await _courseServ.GetAllCourses(page, pageSize, userId);
            var courseVM = _mapper.Map<List<CourseVM>>(coursesDTO.Items);

            var viewCourse = new CourseViewModel()
            {
                Courses = courseVM,
                PageViewModel = new PageViewModel(coursesDTO.Count, page, pageSize)
            };

            return View(viewCourse);
        }


        public async Task<IActionResult> SearchCourses(string? search, int page = 1)
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            int pageSize = 6;

            if (search == null)
            {
                var coursesDTO = await _courseServ.GetAllCourses(page, pageSize, userId);
                var list = _mapper.Map<List<CourseVM>>(coursesDTO.Items);

                var viewCourse = new CourseViewModel()
                {
                    Courses = list,
                    PageViewModel = new PageViewModel(coursesDTO.Count, page, pageSize)
                };
                return PartialView("_CoursesPartial", viewCourse);
            }
            else
            {
                var coursesDTO = await _courseServ.Search(userId, search, page, pageSize);
                var list = _mapper.Map<List<CourseVM>>(coursesDTO.Items);

                var viewCourse = new CourseViewModel()
                {
                    Courses = list,
                    PageViewModel = new PageViewModel(coursesDTO.Count, page, pageSize)
                };

                return PartialView("_CoursesPartial", viewCourse);
            }
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var course = await _courseServ.GetCourseById(id, userId);
            var courseVM = _mapper.Map<CourseVM>(course);
            courseVM.Materials = await _courseServ.GetAllMaterials(userId, id);

            return View(courseVM);
        }

        // GET: Course/Create
        public async Task<IActionResult> Create()
        {
            var courseModel = new AddingCourseModel();
            var materials = await _materialServ.GetAll();
            var skills = await _skillServ.GetAll();
            courseModel.SelectMaterials = materials
                .Select(a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();
            courseModel.SelectSkills = skills
                .Select(a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();
            return View(courseModel);
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddingCourseModel course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            ValidationResult result = await _validator.ValidateAsync(course);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                //add select items
                var materials = await _materialServ.GetAll();
                var skills = await _skillServ.GetAll();
                course.SelectMaterials = materials
                .Select(a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();
                course.SelectSkills = skills
                    .Select(a => new SelectListItem()
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    })
                    .ToList();

                return View("Create", course);
            }
            try
            {
                await _courseServ.AddCourse(course.Name, course.Description, course.MaterialsId.ToArray(), course.SkillsId.ToArray(), User.Identity.Name);
                TempData["notice"] = "Course successfully created";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var course = await _courseServ.GetCourseById(id, userId);
            var materials = await _materialServ.GetAll();
            var skills = await _skillServ.GetAll();
            var courseModel = new AddingCourseModel()
            {
                Id = course.Id,
                Name = course.NameOfCourse,
                Description = course.Description,
            };

            courseModel.SelectMaterials = materials
                .Select(a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();

            courseModel.SelectSkills = skills
                .Select(a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();

            return View(courseModel);
        }

        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddingCourseModel course)
        {
            if (course == null)
            {
                return Content("Wrong model");
            }

            try
            {
                await _courseServ.UpdateCourse(course.Id, course.Name, course.Description, course.MaterialsId.ToArray(), course.SkillsId.ToArray(), User.Identity.Name);
                return RedirectToAction("Details", new { id = course.Id });
            }
            catch
            {
                return View();
            }
        }

        // POST: Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseServ.DeleteCourse(id);
            return RedirectToAction("Index");
        }
    }
}
