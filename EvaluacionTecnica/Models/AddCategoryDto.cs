namespace EvaluacionTecnica.Models
{
    public class AddCategoryDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
