using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pruebatecnica.Data;
using Pruebatecnica.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pruebatecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProductosController(AppDBContext context)
        {
            _context = context;
        }

        // Obtener todos los productos con sus subproductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _context.Productos
                .Include(p => p.SubProductos)
                .ToListAsync();
            return Ok(productos);
        }

        // Crear producto
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductos), new { id = producto.ProductoID }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto updatedProducto)
        {
            if (id != updatedProducto.ProductoID)
                return BadRequest("ID del producto no coincide");

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            producto.NombrePrducto = updatedProducto.NombrePrducto;
            producto.Descripcion = updatedProducto.Descripcion;
            producto.precio = updatedProducto.precio;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> PatchEstado(int id, [FromBody] bool activo)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

    

            await _context.SaveChangesAsync();
            return NoContent();
        }

        
        [HttpPost("{productoId}/subproductos")]
        public async Task<ActionResult<SubProducto>> PostSubProducto(int productoId, SubProducto subProducto)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null)
                return NotFound("El producto no existe");

            subProducto.ProductoID = productoId;
            _context.SubProductos.Add(subProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductos), new { id = productoId }, subProducto);
        }

        // Actualizar subproducto
        [HttpPut("{productoId}/subproductos/{subproductoId}")]
        public async Task<IActionResult> PutSubProducto(int productoId, int subproductoId, SubProducto updatedSubProducto)
        {
            if (subproductoId != updatedSubProducto.SubProductoID || productoId != updatedSubProducto.ProductoID)
                return BadRequest("IDs no coinciden");

            var subProducto = await _context.SubProductos.FindAsync(subproductoId);
            if (subProducto == null)
                return NotFound();

            subProducto.NombreSubPrducto = updatedSubProducto.NombreSubPrducto;
            subProducto.DescripcionSubProducto = updatedSubProducto.DescripcionSubProducto;
            subProducto.PrecioSubProducto = updatedSubProducto.PrecioSubProducto;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Eliminar subproducto
        [HttpDelete("{productoId}/subproductos/{subproductoId}")]
        public async Task<IActionResult> DeleteSubProducto(int productoId, int subproductoId)
        {
            var subProducto = await _context.SubProductos
                .FirstOrDefaultAsync(sp => sp.SubProductoID == subproductoId && sp.ProductoID == productoId);

            if (subProducto == null)
                return NotFound();

            _context.SubProductos.Remove(subProducto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
