using System.ComponentModel.DataAnnotations;

namespace EvaluacionTecnica.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Nombre { get; set; }
        public bool Estado { get; set; } = true;

    }
}
