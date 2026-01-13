using System.Globalization;

namespace Simulation_2.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string Tittle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<Teacher>? Teachers { get; set; }
    }
}
