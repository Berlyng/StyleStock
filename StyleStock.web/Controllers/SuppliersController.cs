using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
			if (!ModelState.IsValid) 
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



	}
}
