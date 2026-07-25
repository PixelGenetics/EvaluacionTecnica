using EvaluacionTecnica.Data;
using EvaluacionTecnica.Models;
using EvaluacionTecnica.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionTecnica.Controllers
{

        [Route("api/products")]
        [ApiController]
        public class ProductController : ControllerBase
        {
            private readonly AppDbContext dbContext;
            public ProductController(AppDbContext dbContext)
            {
                this.dbContext = dbContext;
            }
            [HttpGet]
            public async Task<IActionResult> GetAllProducts()
            {
                var allProduct = await dbContext.Product.Include(p => p.Category).ToListAsync();
                return Ok(allProduct);
            }

        [HttpGet]
            [Route("{ID:Int}")]
            public async Task<IActionResult> GetProductByName(int Id)
            {
                var product = await dbContext.Product.FirstOrDefaultAsync(product => product.Id == Id);

                if (product == null)
                {
                    return NotFound("El nombre que has ingresado no existe como producto");
                }
                return Ok(product);
            }

            [HttpPost]
            public async Task<IActionResult> AddProduct(AddProductDto addProductDto)
            {
                var productEntity = new Product()
                {
                    Codigo = addProductDto.Codigo,
                    Nombre = addProductDto.Nombre,
                    CategoryId = addProductDto.CategoryId,
                    Precio = addProductDto.Precio,
                    Estado = addProductDto.Estado,

                };
                await dbContext.Product.AddAsync(productEntity);
                dbContext.SaveChanges();

                return Ok(productEntity);
            }

            [HttpPut]
            [Route("{ID:int}")]
            public async Task<IActionResult> UpdateProduct(int Id, UpdateProductDto updateProductDto)
            {
                var Product = await dbContext.Product.FindAsync(Id);
                if (Product is null)
                {
                    return NotFound("Categoria no encontrada");
                }

                Product.Codigo = updateProductDto.Codigo;
                Product.Nombre = updateProductDto.Nombre;
                Product.Precio = updateProductDto.Precio;
                Product.Estado = updateProductDto.Estado;

                dbContext.SaveChanges();

                return Ok(Product);
            }

            [HttpDelete]
            [Route("{ID:int}")]
            public async Task<IActionResult> DeleteProduct(int Id)
            {
                var Product = await dbContext.Product.FirstOrDefaultAsync(Product => Product.Id == Id);
                if (Product is null)
                {
                    return NotFound();
                }

                dbContext.Product.Remove(Product);
                await dbContext.SaveChangesAsync();

                return Ok("Producto eliminado");
            }
        }
    }
