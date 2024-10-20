using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StyleStock.Domain.Entities;
using StyleStock.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StyleStock.Domain.DTOS;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace StyleStock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly StyleStockDbContext _context;

        public SuppliersController(StyleStockDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetallSuppliers")]
        public async Task<ActionResult<List<Suppliers>>> GetallSuppliers()
        {
            var suppliers = await _context.Suppliers.ToListAsync();

            if (suppliers == null || suppliers.Count == 0)
            {
                return NotFound("No suppliers found.");
            }

            return Ok(suppliers);
        }

        [HttpPost("CreateSuppliers")]
        public async Task<IActionResult> CreateSuppliers(CreateSuppliersDTO supplierDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Suppliers newSupplier = new Suppliers
            {
                Name = supplierDTO.Name,
                Phone = supplierDTO.Phone,
                Email = supplierDTO.Email,
                Adress = supplierDTO.Adress
            };

            _context.Suppliers.Add(newSupplier);
            await _context.SaveChangesAsync();

            return Ok(newSupplier);
        }

        [HttpPut("SuppliersEdit/{SupplierID}")]
        public async Task<IActionResult> SuppliersEdit(int SupplierID, UpdateSuppliersDTO updateSuppliers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supplier = await _context.Suppliers.FindAsync(SupplierID);

            if (supplier == null)
            {
                return NotFound(new { message = $"El Suplidor con el ID {SupplierID} no fue encontrado" });
            }


            supplier.Name = updateSuppliers.Name;
            supplier.Phone = updateSuppliers.Phone;
            supplier.Email = updateSuppliers.Email;
            supplier.Adress = updateSuppliers.Adress;


            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();

            return Ok(supplier);
        }

        [HttpDelete("SuppliersDelete")]
        public async Task<IActionResult> SuppliersDelete(int SupplierID)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierID == SupplierID);

            if (supplier == null)
            {
                return NotFound(new { message = $"El Suplidor con el ID {SupplierID} no fue encontrado" });
            }
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return Ok( new {message = $"El registro {SupplierID} ha sido eliminado con exito"});
        }
    }
}

