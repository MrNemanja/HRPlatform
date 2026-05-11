using HRPlatform.DTOs;
using HRPlatform.Mappers;
using HRPlatform.Models;
using HRPlatform.Repositories;

namespace HRPlatform.Services
{
    public class SkillService: ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
          
        public async Task<SkillDTO> AddSkillAsync(CreateSkillDTO createSkillDTO)
        {

            Skill existingSkill = await _skillRepository.GetSkillByNameAsync(createSkillDTO.Name);
            if (existingSkill != null)
            {
                throw new InvalidOperationException("Skill with this name already exists.");
            }

            Skill skill = SkillMapper.ToEntity(createSkillDTO);

            Skill createdSkill = await _skillRepository.AddSkillAsync(skill);

            return SkillMapper.ToDTO(createdSkill);
        }
    }
}
