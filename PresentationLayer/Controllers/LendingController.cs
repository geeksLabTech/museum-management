using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
//using System.Text;

namespace museum_management.Controllers{

    public class LendingController: Controller{
            
            private readonly MuseumManagementContext _context;
    
            public LendingController(MuseumManagementContext context) {
                _context = context;
            }
    
            public async Task<IActionResult> Index() {
                var lendingToMuseums = _context.LendingToMuseums.ToList();
    
                return View(lendingToMuseums);
            }
    
    }

}