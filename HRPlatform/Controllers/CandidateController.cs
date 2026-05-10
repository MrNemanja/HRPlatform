using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRPlatform.DTOs;
using HRPlatform.Services;

namespace HRPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate([FromBody] CreateCandidateDTO candidateDTO)
        {
            try
            {
                CandidateDTO createdCandidateDTO = await _candidateService.AddCandidateAsync(candidateDTO);
                return StatusCode(201, createdCandidateDTO);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{candidateId}")]
        public async Task<IActionResult> RemoveCandidate(int candidateId)
        {
            try
            {
                await _candidateService.DeleteCandidateAsync(candidateId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{candidateId}/skills/{skillId}")]
        public async Task<IActionResult> AddSkillToCandidate(int candidateId, int skillId)
        {
            try
            {
                await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{candidateId}/skills/{skillId}")]
        public async Task<IActionResult> RemoveSkillFromCandidate(int candidateId, int skillId)
        {
            try
            {
                await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCandidates(
            [FromQuery] string? name,
            [FromQuery] List<string>? skills
        )
        {
            try
            {
                List<CandidateDTO> filteredCandidates = await _candidateService.SearchCandidatesAsync(name, skills);
                return Ok(filteredCandidates);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
