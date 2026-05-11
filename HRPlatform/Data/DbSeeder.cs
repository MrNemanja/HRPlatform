using HRPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext dbContext)
        {
            dbContext.Database.Migrate();

            if (!dbContext.Skills.Any())
            {
                List<Skill> skills = new List<Skill>
                {
                    new Skill { Name = "C#" },
                    new Skill { Name = "React" },
                    new Skill { Name = "SQL" },
                    new Skill { Name = "JavaScript" },
                    new Skill { Name = "English" },
                    new Skill { Name = "Serbian" }
                };

                dbContext.Skills.AddRange(skills);
                dbContext.SaveChanges();
            }

            if (!dbContext.Candidates.Any())
            {
                List<Skill> skills = dbContext.Skills.ToList();

                Skill csharp = skills.First(s => s.Name == "C#");
                Skill react = skills.First(s => s.Name == "React");
                Skill sql = skills.First(s => s.Name == "SQL");
                Skill js = skills.First(s => s.Name == "JavaScript");
                Skill english = skills.First(s => s.Name == "English");
                Skill serbian = skills.First(s => s.Name == "Serbian");

                List<Candidate> candidates = new List<Candidate>
                {
                    new Candidate
                    {
                        FullName = "Nemanja Nikolic",
                        DateOfBirth = new DateTime(1997, 11, 1),
                        ContactNumber = "+381651234567",
                        Email = "nemanja@gmail.com",
                        Skills = new List<Skill> { csharp, sql, english, serbian }
                    },
                    new Candidate
                    {
                        FullName = "Marko Markovic",
                        DateOfBirth = new DateTime(1995, 5, 10),
                        ContactNumber = "+381641234567",
                        Email = "marko@gmail.com",
                        Skills = new List<Skill> { react, js }
                    },
                    new Candidate
                    {
                        FullName = "Petar Petrovic",
                        DateOfBirth = new DateTime(1998, 3, 22),
                        ContactNumber = "+381631234567",
                        Email = "petar@gmail.com",
                        Skills = new List<Skill> { csharp, serbian }
                    }
                };

                dbContext.Candidates.AddRange(candidates);
                dbContext.SaveChanges();

            }

            Console.WriteLine("Seeder finished");
        }
    }
}
