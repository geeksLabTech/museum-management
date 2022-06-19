using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using PresentationLayer.ViewModels;
using DataLayer.Models.Auth;
using Microsoft.AspNetCore.Authorization;
namespace museum_management.Controllers{
    [Authorize (Roles = UserRoles.Restaurator)]
    public class RestaurationController: Controller{
            
            private readonly MuseumManagementContext _context;
    
            public RestaurationController(MuseumManagementContext context) {
                _context = context;
            }
    
            public async Task<IActionResult> Index() {
                
                var restaurations = await _context.Restaurations.ToListAsync();
                List<RestaurationViewModel> restaurationViewModels = new List<RestaurationViewModel>();
                foreach(var restauration in restaurations){
                    var artwork = await _context.Artworks.FindAsync(restauration.ArtworkId);
                    restaurationViewModels.Add(new RestaurationViewModel{
                        ArtworkTitle = restauration.Artwork.Title,
                        Restauration = restauration
                    });
                }

                return View(restaurationViewModels);
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