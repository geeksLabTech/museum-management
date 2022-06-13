using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using museum_management.Data;
using System.Text.Encodings.Web;
using museum_management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace museum_management.Controllers{
    public class CatalogController : Controller {
        private readonly MuseumManagementContext _context;

        public CatalogController(MuseumManagementContext context) {
            _context = context;
        }
        
        public async Task<IActionResult> Index() {
            var artworks = await _context.Artworks.ToListAsync();
            var restaurations = await _context.Restaurations.ToListAsync();
            var lendingToMuseums = await _context.LendingToMuseums.ToListAsync();

            var artworkViewModelList = new List<ArtworkViewModel>();

            foreach (var artwork in artworks) {
                var artworkViewModel = new ArtworkViewModel();
                artworkViewModel.Artwork = artwork;
                artworkViewModel.Restaurations = new List<Restauration>();
                artworkViewModel.ActualMuseum = "My Museum";
                foreach (var restauration in restaurations) {
                    if (restauration.ArtworkId == artwork.Id) {
                        artworkViewModel.Restaurations.Add(restauration);
                    }
                }
                /*
                foreach (var lendingToMuseum in lendingToMuseums) {
                    if (lendingToMuseum.ArtworkId == artwork.Id && !lendingToMuseum.IsFinished) {
                        artworkViewModel.ActualMuseum = lendingToMuseum.Museum.Name;
                    }
                }
                */
                artworkViewModelList.Add(artworkViewModel);
            } 

            return View(artworkViewModelList);
        }

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

        //public IActionResult Create(){
            
        //}

        public async Task<IActionResult> Edit(int? id){
            if (id == null) {
                return NotFound();
            }

            var artwork = await _context.Artworks.FirstOrDefaultAsync(m => m.Id == id);

            if (artwork == null) {
                return NotFound();
            }

            return View(artwork);
        }

        public async Task<IActionResult> Delete(int? id){
            if (id == null) {
                return NotFound();
            }

            var artwork = await _context.Artworks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artwork == null) {
                return NotFound();
            }

            return View(artwork);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id){
            var artwork = await _context.Artworks.FindAsync(id);
            _context.Artworks.Remove(artwork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        
        public async Task<IActionResult> Index(string artworkroom)  
        {
  
            IQueryable<string> genreQuery = from m in _context.Artworks
                                            orderby m.MuseumRoom
                                            select m.MuseumRoom;
            var artwork = from m in _context.Artworks
                         select m;


            if (!string.IsNullOrEmpty(artworkroom))
            {
                artwork = artwork.Where(x => x.MuseumRoom == artworkroom);
            }

            var artworkRoomVM = new ArtworkRoomViewModel
            {
                MuseumRoom = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Artworks = await artwork.ToListAsync()
            };

            return View(artworkRoomVM);
}
    }
}