using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using StyleStock.Domain;
using StyleStock.Domain.DTOS;
using StyleStock.Domain.Entities;

namespace StyleStock.web.Controllers
{
    public class UsersController : Controller
    {
        private readonly StyleStockDbContext _context;

        public UsersController(StyleStockDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Users> users = _context.Users.ToList();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUsersDTO userDTO)
        {
			if (!ModelState.IsValid)
			{
				return View(userDTO);
			}

            var passworHasher = new PasswordHasher<Users>();

			var newUser = new Users
			{
				FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
				Role = userDTO.Role,
			};

			newUser.PasswordHash = passworHasher.HashPassword(newUser, userDTO.PasswordHash);




			_context.Users.Add(newUser);
			await _context.SaveChangesAsync();


			return RedirectToAction(nameof(Index));
		}


        [HttpGet]
        public async Task<IActionResult> UserEdit(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync( u => u.UserID == id);
            if (user == null) 
            {
                return NotFound();
            }

            var userDTO = new UpdateUsersDTO()
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
            };

            return View(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UpdateUsersDTO userDTO) 
        {
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }

            var user = await _context.Users.FindAsync(userDTO.UserID);
            if (user == null) 
            {
                return NotFound();
            }

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.Role = userDTO.Role;

			if (!string.IsNullOrWhiteSpace(userDTO.PasswordHash))
				{
					var passwordHasher = new PasswordHasher<Users>();
					user.PasswordHash = passwordHasher.HashPassword(user, userDTO.PasswordHash);
				
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
		}



	}
}
