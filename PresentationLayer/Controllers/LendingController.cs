using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using PresentationLayer.ViewModels;
//using System.Text;

namespace museum_management.Controllers{

    public class LendingController: Controller{
            
            private readonly MuseumManagementContext _context;
    
            public LendingController(MuseumManagementContext context) {
                _context = context;
            }
    
            public async Task<IActionResult> Index() {
                
                var lendingToMuseums = await _context.LendingToMuseums.ToListAsync();
                List<LendingViewModel> lendingViewModels = new List<LendingViewModel>();

                foreach (var lending in lendingToMuseums) {
                    var artwork = await _context.Artworks.FindAsync(lending.ArtworkId);
                    var museum = await _context.Museums.FindAsync(lending.MuseumId);
                    lendingViewModels.Add(new LendingViewModel {
                        LendingToMuseum = lending,
                        ArtworkTitle = artwork.Title,
                        MuseumName = museum.Name,
                        
                    });
                
                }
                System.Console.WriteLine(  "mmmm");
                System.Console.WriteLine("LendingToMuseums: " + lendingToMuseums[0].ArtworkId);
                return View(lendingViewModels);
            }
    
    }

}