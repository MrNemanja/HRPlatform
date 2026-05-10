using HRPlatform.DTOs;

namespace HRPlatform.Services
{
    public interface ISkillService
    {
        Task<SkillDTO> AddSkillAsync(CreateSkillDTO createSkillDTO);
    }
}
