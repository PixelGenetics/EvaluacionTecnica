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
    }
}
