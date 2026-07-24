using EvaluacionTecnica.Data;
using EvaluacionTecnica.Models;
using EvaluacionTecnica.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        public CategoryController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var allCategories = await dbContext.Category.ToListAsync();
            return Ok(allCategories);
        }

        [HttpGet]
        [Route("Nombre")]
        public async Task<IActionResult> GetCategoryByName(string Nombre)
        {
            var category = await dbContext.Category.FirstOrDefaultAsync(category => category.Nombre == Nombre);

            if (category == null)
            {
                return NotFound("El nombre que has ingresado no existe como categoria");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDto addCategoryDto)
        {
            var categoryEntity = new Category()
            {
                Nombre = addCategoryDto.Nombre,
                Estado = addCategoryDto.Estado
            };
            await dbContext.Category.AddAsync(categoryEntity);
            dbContext.SaveChanges();

            return Ok(categoryEntity);
        }
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateCategory(int Id,UpdateCategoryDto updateCategoryDto) 
        {
            var Category = await dbContext.Category.FindAsync(Id);
            if (Category is null)
            {
                return NotFound("Categoria no encontrada");
            }
            
            Category.Nombre = updateCategoryDto.Nombre;
            Category.Estado = updateCategoryDto.Estado;

            dbContext.SaveChanges();

            return Ok(Category);
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            var Category = await dbContext.Category.FirstOrDefaultAsync(Category => Category.Id == Id);
            if (Category is null)
            {
                return NotFound();
            }

            var hasProducts = await dbContext.Product.AnyAsync(p => p.CategoryId == Id);

            if (hasProducts)
            {
                return Conflict(new
                {
                    message = "No se puede eliminar la categoria porque tiene productos asociados."
                });
            }

            dbContext.Category.Remove(Category);
            await dbContext.SaveChangesAsync();

            return Ok("Categoria eliminada");
        }
    }
}
