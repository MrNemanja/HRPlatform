using System.ComponentModel.DataAnnotations;

namespace HRPlatform.Models
{
    public class Skill
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
