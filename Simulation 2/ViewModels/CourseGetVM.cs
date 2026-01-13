using System.ComponentModel.DataAnnotations;

namespace Simulation_2.ViewModels
{
    public class CourseGetVM
    {
        [Required]
        public int Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        [MaxLength(256)]
        public string Tittle { get; set; } = string.Empty;
        [MaxLength(512)]
        public string Description { get; set; } = string.Empty;
    }
}
