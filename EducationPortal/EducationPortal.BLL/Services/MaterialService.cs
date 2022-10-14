using AutoMapper;
using EducationPortal.BLL.DTO;
using EducationPortal.BLL.Infrastructure;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.Mapper;
using EducationPortal.DAL.DataContext;
using EducationPortal.DAL.Entities;
using EducationPortal.DAL.Interfaces;
using EducationPortal.DAL.Repositories;

namespace EducationPortal.BLL.Services
{
    public class MaterialService : IMaterialService
    {
        private IUnitOfWork _uof;
        private IMapper _mapper;

        public MaterialService(PortalContext context)
        {
            _uof = new EFUnitOfWork(context);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        public async Task<MaterialDTO> Get(int id)
        {
            var material = await _uof.Materials.Get(id);

            if (material is ArticleMaterial article)
            {
                return _mapper.Map<ArticleMaterialDTO>(article);
            }
            else if (material is BookMaterial book)
            {
                return _mapper.Map<BookMaterialDTO>(book);
            }
            else if (material is VideoMaterial video)
            {
                return _mapper.Map<VideoMaterialDTO>(video);
            }
            else
            {
                throw new Exception("Some wrong with material type");
            }
        }

        public async Task AddMaterial(MaterialDTO materialDTO)
        {
            if (materialDTO is BookMaterialDTO book)
            {
                AddBook(book);
            }
            else if (materialDTO is ArticleMaterialDTO article)
            {
                AddArticle(article);
            }
            else if (materialDTO is VideoMaterialDTO video)
            {
                AddVideo(video);
            }
            else
            {
                throw new Exception("Some wrong with type of material");
            }
        }

        public async Task <List<MaterialDTO>> GetAll()
        {
            var listOfmaterialsDTO = new List <MaterialDTO>();
            var materials = await _uof.Materials.GetAll();
            foreach (var mat in materials)
            {
                if (mat is BookMaterial book)
                {
                    var materialDTO = _mapper.Map<BookMaterialDTO>(book);
                    listOfmaterialsDTO.Add(materialDTO);
                }

                if (mat is ArticleMaterial article)
                {
                    var materialDTO = _mapper.Map<ArticleMaterialDTO>(article);
                    listOfmaterialsDTO.Add(materialDTO);
                }

                if (mat is VideoMaterial video)
                {
                    var materialDTO = _mapper.Map<VideoMaterialDTO>(video);
                    listOfmaterialsDTO.Add(materialDTO);
                }
            }

            return listOfmaterialsDTO;
        }

        //Добавить редактирование разных материалов
        public async Task Update(MaterialDTO newMaterialDTO)
        {
            if (newMaterialDTO == null)
            {
                throw new ValidationException("material is null", "");
            }

            Material material = await _uof.Materials.Get(newMaterialDTO.Id);
            if (material == null)
            {
                throw new ValidationException("Object don't Exist", "");
            }

            switch (newMaterialDTO)
            {
                case ArticleMaterialDTO articleDTO:
                    var article = _mapper.Map<ArticleMaterial>(articleDTO);
                    await _uof.Materials.Update(newMaterialDTO.Id, article);
                    await _uof.Save();
                    return;
                case BookMaterialDTO bookDTO:
                    var book = _mapper.Map<BookMaterial>(bookDTO);
                    await _uof.Materials.Update(newMaterialDTO.Id, book);
                    await _uof.Save();
                    return;
                case VideoMaterialDTO videoDTO:
                    var video = _mapper.Map<VideoMaterial>(videoDTO);
                    await _uof.Materials.Update(newMaterialDTO.Id, video);
                    await _uof.Save();
                    return;
                default:
                    throw new ValidationException("Wrong type of material", "");
            }
        }

        //Временный метод
        public async Task Update(int id, string newName)
        {
            Material material = await _uof.Materials.Get(id);
            if (material == null)
            {
                throw new ValidationException("Object don't Exist", "");
            }

            var newMaterial = new Material()
            {
                Id = id,
                Name = newName,
                Courses = material.Courses,
                Users = material.Users
            };

            await _uof.Materials.Update(id, newMaterial);
            await _uof.Save();
        }

        //need validation
        public async Task Delete(int id)
        {
            await _uof.Materials.Delete(id);
            await _uof.Save();
        }

        private async Task AddArticle(ArticleMaterialDTO articleDTO)
        {
            if (articleDTO == null)
            {
                throw new ValidationException("Article is null", "");
            }

            var article = _mapper.Map<ArticleMaterial>(articleDTO);
            await _uof.Materials.Create(article);
            await _uof.Save();
        }

        private async Task AddBook(BookMaterialDTO bookDTO)
        {
            if (bookDTO == null)
            {
                throw new ValidationException("Book is null", "");
            }

            var book = _mapper.Map<BookMaterial>(bookDTO);
            await _uof.Materials.Create(book);
            await _uof.Save();
        }

        private async Task AddVideo(VideoMaterialDTO videoDTO)
        {
            if (videoDTO == null)
            {
                throw new ValidationException("Video is null", "");
            }

            var video = _mapper.Map<VideoMaterial>(videoDTO);
            await _uof.Materials.Create(video);
            await _uof.Save();
        }
    }
}