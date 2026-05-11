using System.ComponentModel.DataAnnotations;

namespace HRPlatform.DTOs
{
    public class CreateSkillDTO
    {
        [Required(ErrorMessage = "Skill name is required.")]
        [MaxLength(100, ErrorMessage = "Skill name cannot be longer than 100 characters.")]
        public string Name { get; set; }
    }
}
