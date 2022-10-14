using EducationPortal.BLL.DTO;
using EducationPortal.BLL.Interfaces;
using EducationPortal.DAL.Entities;
using EducationPortal.Web.Validators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Web.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        private readonly IMaterialService _materialServ;
        private readonly IValidator<ArticleMaterialDTO> _validatorArticle;
        private readonly IValidator<BookMaterialDTO> _validatorBook;
        private readonly IValidator<VideoMaterialDTO> _validatorVideo;
        private readonly UserManager<User> _userManager;
        public MaterialController(UserManager<User> usermanager, IMaterialService materialService, IValidator<VideoMaterialDTO> validatorV, IValidator<ArticleMaterialDTO> validatorA, IValidator<BookMaterialDTO> validatorB)
        {
            _userManager = usermanager; 
            _validatorArticle = validatorA;
            _validatorBook = validatorB;
            _validatorVideo = validatorV;
            _materialServ = materialService;
        }

        public async Task<IActionResult> Index()
        {
            var materials = await _materialServ.GetAll();

            return View(materials);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //public async Task<IActionResult> ShowMaterial()
        //{
        //    return PartialView("_ShowMaterialPartial");
        //}

        public async Task<IActionResult> AddArticle()
        {
            return PartialView("Partial/AddArticlePartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddArticle(ArticleMaterialDTO article)
        {

            if (article == null)
            {
                throw new ValidationException("ArticleMaterial");
            }

            ValidationResult result = await _validatorArticle.ValidateAsync(article);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return View("Create", article);
            }

            article.Created = DateTime.Now;
            article.UserNameCreate = User.Identity.Name;

            await _materialServ.AddMaterial(article);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddBook()
        {
            return PartialView("Partial/AddBookPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(BookMaterialDTO book)
        {

            if (book == null)
            {
                throw new ValidationException("BookMaterial");
            }

            ValidationResult result = await _validatorBook.ValidateAsync(book);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return View("Create", book);
            }

            book.Created = DateTime.Now;
            book.UserNameCreate = User.Identity.Name;

            await _materialServ.AddMaterial(book);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddVideo()
        {
            return PartialView("Partial/AddVideoPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVideo(VideoMaterialDTO video)
        {
            if (video == null)
            {
                throw new ValidationException("BookMaterial");
            }

            ValidationResult result = await _validatorVideo.ValidateAsync(video);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return View("Create", video);
            }

            video.Created = DateTime.Now;
            video.UserNameCreate = User.Identity.Name;

            await _materialServ.AddMaterial(video);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var material = await _materialServ.Get((int)id);

            switch (material)
            {
                case ArticleMaterialDTO article:
                    return View("ArticleDetails", article);
                case BookMaterialDTO book:
                    return View("BookDetails", book);
                case VideoMaterialDTO video:
                    return View("VideoDetails", video);
                default:
                    return BadRequest();
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var material = await _materialServ.Get((int)id);

            switch (material)
            {
                case ArticleMaterialDTO article:
                    return View("Edit/EditArticle", article);
                case BookMaterialDTO book:
                    return View("Edit/EditBook", book);
                case VideoMaterialDTO video:
                    return View("Edit/EditVideo", video);
                default:
                    return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticle(ArticleMaterialDTO article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }
            
            article.UserNameCreate = User.Identity.Name;
            article.Created = DateTime.Now;

            await _materialServ.Update(article);
            return RedirectToAction("Details", new { id = article.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(BookMaterialDTO book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            book.UserNameCreate = User.Identity.Name;
            book.Created = DateTime.Now;

            await _materialServ.Update(book);
            return RedirectToAction("Details", new { id = book.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVideo(VideoMaterialDTO video)
        {
            if (video == null)
            {
                throw new ArgumentNullException(nameof(video));
            }

            video.UserNameCreate = User.Identity.Name;
            video.Created = DateTime.Now;

            await _materialServ.Update(video);
            return RedirectToAction("Details", new { id = video.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (int? id)
        {
            if (!id.HasValue)
            {
                return Content("Don't have a id parametr");
            }

            await _materialServ.Delete(id.Value);
            return RedirectToAction("Index");
        }
    }
}
