using System.ComponentModel.DataAnnotations;

namespace HRPlatform.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();

    }
}
