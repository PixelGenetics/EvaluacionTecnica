using System.ComponentModel.DataAnnotations;

namespace EvaluacionTecnica.Models
{
    public class UpdateCategoryDto
    {
        [StringLength(100)]
        public required string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
