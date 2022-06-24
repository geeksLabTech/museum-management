using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using System.Text.Encodings.Web;
using DataLayer.Models;
using PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using DataLayer.Models.Auth;
using DataLayer;

namespace museum_management.Controllers{
    [AllowAnonymous]
    public class CatalogController : Controller {
        
        private readonly DataLayer.UnitOfWork.IUnitOfWork _unitOfWork;

        public CatalogController(Datalayer.UnitOfWork.UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        
        // public async Task<IActionResult> Index() {
            // var artworks = await _context.Artworks.ToListAsync();
            // var restaurations = await _context.Restaurations.ToListAsync();
            // var lendingToMuseums = await _context.LendingToMuseums.ToListAsync();
// 
            // var artworkViewModelList = new List<ArtworkViewModel>();
// 
            // foreach (var artwork in artworks) {
                // var artworkViewModel = new ArtworkViewModel();
                // artworkViewModel.Artwork = artwork;
                // artworkViewModel.Restaurations = new List<Restauration>();
                // artworkViewModel.ActualMuseum = "My Museum";
                // foreach (var restauration in restaurations) {
                    // if (restauration.ArtworkId == artwork.Id) {
                        // artworkViewModel.Restaurations.Add(restauration);
                    // }
                // }
                // /*
                // foreach (var lendingToMuseum in lendingToMuseums) {
                    // if (lendingToMuseum.ArtworkId == artwork.Id && !lendingToMuseum.IsFinished) {
                        // artworkViewModel.ActualMuseum = lendingToMuseum.Museum.Name;
                    // }
                // }
                // */
                // artworkViewModelList.Add(artworkViewModel);
            // } 
// 
            // return View(artworkViewModelList);
        // }

        public IActionResult Restaurations(int id) {
            // if (id == null) {
            //     return NotFound();
            // }

            var restaurations = _unitOfWork.Restaurations.GetById(id);
            // Where(r => r.ArtworkId == id).ToListAsync();

            if (restaurations == null) {
                return NotFound();
            }
            
            return View(restaurations);
            

        }

        //public IActionResult Create(){
            
        //}

        public IActionResult Edit(int id){
        

            var artwork = _unitOfWork.Artworks.GetById(id);
            // FirstOrDefaultAsync(m => m.Id == id);

            if (artwork == null) {
                return NotFound();
            }

            return View(artwork);
        }

        public IActionResult Delete(int id){
        
            var artwork = _unitOfWork.Artworks.GetById(id);
                // .FirstOrDefaultAsync(m => m.Id == id);
            if (artwork == null) {
                return NotFound();
            }

            return View(artwork);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id){
            var artwork = _unitOfWork.Artworks.GetById(id);
            _unitOfWork.Artworks.Remove(artwork);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
        
        
        public IActionResult Index(string artworkroom)  
        {
            
            var roomQuery = from m in _unitOfWork.Artworks.GetAll()
                                            orderby m.MuseumRoom
                                            select m.MuseumRoom;
            var artwork = from m in _unitOfWork.Artworks.GetAll()
                         select m;


            if (!string.IsNullOrEmpty(artworkroom))
            {
                artwork = artwork.Where(x => x.MuseumRoom == artworkroom);
            }

            var artworkRoomVM = new ArtworkRoomViewModel
            {
                MuseumRoom = new SelectList( roomQuery.Distinct().ToList()),
                Artworks = artwork.ToList()
            };

            return View(artworkRoomVM);
        }
    }
}