using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;

namespace museum_management.Controllers{

    public class RolesController : Controller {

        private readonly MuseumManagementContext _context;

        public RolesController(MuseumManagementContext context) {
            _context = context;
        }

        public IActionResult Index() {
            var users = _context.Users.ToList();

            return View(users);
        }

    }
}
