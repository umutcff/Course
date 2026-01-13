using System.ComponentModel.DataAnnotations;

namespace Simulation_2.ViewModels
{
    public class CourseUpdateVM
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; } = null!;
        [MaxLength(256)]
        public string Tittle { get; set; } = string.Empty;
        [MaxLength(512)]
        public string Description { get; set; } = string.Empty;
    }
}
