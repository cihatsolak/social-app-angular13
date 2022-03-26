namespace ServerApp.Controllers
{
    [Authorize]
    public class ProductsController : BaseApiController
    {
        private readonly SocialDbContext _context;

        public ProductsController(SocialDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            if (!products.Any())
                return NotFound();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Add), product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            try
            {
                var productEntity = await _context.Products.FindAsync(id);
                if (productEntity is null)
                    return NotFound();

                productEntity.Name = product.Name;
                productEntity.Price = product.Price;
                productEntity.IsActive = product.IsActive;

                _context.Products.Update(productEntity);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var productEntity = await _context.Products.FindAsync(id);
                if (productEntity is null)
                    return NotFound();

                _context.Products.Remove(productEntity);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return Problem();
            }
        }
    }
}