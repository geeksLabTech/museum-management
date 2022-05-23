using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using museum_management.Data;

namespace museum_management.Controllers{

    class LendingController: Controller{
            
            private readonly MuseumManagementContext _context;
    
            public LendingController(MuseumManagementContext context) {
                _context = context;
            }
    
            public IActionResult Index() {
                var lendingToMuseums = _context.LendingToMuseums.ToList();
    
                return View(lendingToMuseums);
            }
    
    }

}