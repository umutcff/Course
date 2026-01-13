namespace Simulation_2.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public int CourseID { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public Course? Course { get; set; }
    }
}
