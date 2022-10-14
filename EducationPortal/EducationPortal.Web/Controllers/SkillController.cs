using EducationPortal.BLL.DTO;
using EducationPortal.BLL.Interfaces;
using EducationPortal.Web.Validators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Web.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        private readonly ISkillService _skill;
        private readonly IValidator <SkillDTO> _validator;

        public SkillController(ISkillService skillService, IValidator<SkillDTO> validator)
        {
            _validator = validator;
            _skill = skillService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _skill.GetAll());
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SkillDTO skillDTO)
        {
            if (skillDTO == null)
            {
                return View("Error");
            }

            ValidationResult result = await _validator.ValidateAsync(skillDTO);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return View("Create", skillDTO);
            }
            
            skillDTO.Created = DateTime.Now;
            skillDTO.UserNameCreate = User.Identity.Name;

            await _skill.AddSkill(skillDTO);

            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Edit (int? id)
        {
            if (id != null)
            {
                SkillDTO skillDto = await _skill.GetSkill((int)id);

                return View(skillDto);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (SkillDTO skillDTO)
        {
            if (skillDTO == null)
            {
                return Content("NEED CORECT IT");
            }

            ValidationResult result = await _validator.ValidateAsync(skillDTO);


            skillDTO.UserNameCreate = User.Identity.Name;
            skillDTO.Created = DateTime.Now;

            await _skill.Update(skillDTO);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                //need validation check
                await _skill.DeleteSkill((int)id);
                return RedirectToAction("Index");
            }
            return Content("SOME WRONG, EDIT IT");
        }

        public async Task<IActionResult> Details(int? id)
        {
            var skill = await _skill.GetSkill((int)id);
            return View(skill);
        }
    }
}