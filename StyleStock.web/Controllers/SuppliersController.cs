using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StyleStock.Domain;
using StyleStock.Domain.DTOS;
using StyleStock.Domain.Entities;

namespace StyleStock.web.Controllers
{
	public class SuppliersController : Controller
	{
		private readonly StyleStockDbContext _context;

		public SuppliersController(StyleStockDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{

			List<Suppliers> suppliers = await _context.Suppliers.ToListAsync();

			return View(suppliers);

		}

		[HttpGet]
		public IActionResult CreateSuppliers()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateSuppliers(CreateSuppliersDTO supplierDTO)
		{
			if (!ModelState.IsValid || ModelState.IsNullOrEmpty())
			{
				return View(supplierDTO);
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


			return RedirectToAction(nameof(Index));

		}

		[HttpGet]
		public async Task<IActionResult> SuppliersEdit(int id)
		{
			var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierID == id);

			if (supplier == null)
			{
				return NotFound();
			}

			var updateSuppliersDTO = new UpdateSuppliersDTO
			{
				SupplierID = supplier.SupplierID,
				Name = supplier.Name,
				Phone = supplier.Phone,
				Email = supplier.Email,
				Adress = supplier.Adress
			};

			return View(updateSuppliersDTO);
		}

		[HttpPost]
		public async Task<IActionResult> SuppliersEdit(UpdateSuppliersDTO updateSuppliers)
		{
			if (!ModelState.IsValid)
			{
				return View(updateSuppliers);
			}

			var supplier = await _context.Suppliers.FindAsync(updateSuppliers.SupplierID);

			if (supplier == null)
			{
				return NotFound();
			}

			supplier.Name = updateSuppliers.Name;
			supplier.Phone = updateSuppliers.Phone;
			supplier.Email = updateSuppliers.Email;
			supplier.Adress = updateSuppliers.Adress;

			_context.Suppliers.Update(supplier);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));

		}

		[HttpGet]
		public async Task<IActionResult> SuppliersDelete (int id)
		{
			var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierID == id);

			if (supplier == null)
			{
				return NotFound();
			}
			_context.Suppliers.Remove(supplier);
			await _context.SaveChangesAsync();


			return RedirectToAction(nameof(Index));
		}

	}
}
