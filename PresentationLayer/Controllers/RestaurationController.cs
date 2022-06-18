using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;

namespace museum_management.Controllers{

    public class RestaurationController: Controller{
            
            private readonly MuseumManagementContext _context;
    
            public RestaurationController(MuseumManagementContext context) {
                _context = context;
            }
    
            public IActionResult Index() {
                
                var restaurations = _context.Restaurations.ToList();
                
                return View(restaurations);
               
                    
            }
            /*
            public async Task<IActionResult> Restaurations(int? id) {
                if (id == null) {
                    return NotFound();
                }
    
                var restaurations = await _context.Restaurations.Where(r => r.ArtworkId == id).ToListAsync();
    
                if (restaurations == null) {
                    return NotFound();
                }
    
                return View(restaurations);
    
            }
            */
    }
}