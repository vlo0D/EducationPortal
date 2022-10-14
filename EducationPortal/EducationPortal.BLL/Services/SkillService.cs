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
    public class SkillService : ISkillService
    {
        private IUnitOfWork _uof;
        private IMapper _mapper;

        public SkillService(PortalContext context)
        {
            _uof = new EFUnitOfWork(context);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        public async Task<List<SkillDTO>> GetAll()
        {
            List<Skill> result = await _uof.Skills.GetAll();
            var skillsDTO = _mapper.Map<List<SkillDTO>>(result);
            return skillsDTO;
        }

        public async Task AddSkill(SkillDTO skillDTO)
        {
            if (skillDTO == null)
            {
                throw new ValidationException("Skill is null", "");
            }

            await _uof.Skills.Create(_mapper.Map<Skill>(skillDTO));
            await _uof.Save();
        }

        public async Task<SkillDTO> GetSkill(int id)
        {
            var skill = await _uof.Skills.Get(id);
            var skillDTO = _mapper.Map<SkillDTO>(skill);
            return skillDTO;
        }

        public async Task Update(SkillDTO skillDTO)
        {
            if (skillDTO is null)
            {
                throw new Exception("skillDTO null");
            }

            await _uof.Skills.Update(skillDTO.Id, _mapper.Map<Skill>(skillDTO));
            await _uof.Save();
        }

        public async Task DeleteSkill(int id)
        {
            await _uof.Skills.Delete(id);
            await _uof.Save();
        }
    }
}
