using HRPlatform.DTOs;
using HRPlatform.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill([FromBody] CreateSkillDTO createSkillDTO)
        {
            try
            {
                SkillDTO createdskillDTO = await _skillService.AddSkillAsync(createSkillDTO);
                return StatusCode(201, createdskillDTO);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
