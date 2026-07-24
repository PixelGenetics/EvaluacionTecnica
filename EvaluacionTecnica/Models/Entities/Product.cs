using System.ComponentModel.DataAnnotations;

namespace EvaluacionTecnica.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]

        public string Codigo { get; set; } = string.Empty;
        [Required]
        [MaxLength(150)]

        public string Nombre { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        [Range(0,double.MaxValue)]

        public decimal Precio { get; set; }

        public bool Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public Category Category { get; set; } = null!;
    }
}
