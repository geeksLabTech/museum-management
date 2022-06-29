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
using DataLayer.UnitOfWork;


namespace museum_management.Controllers{
    [AllowAnonymous]
    public class CatalogController : Controller {
        
        private readonly IUnitOfWork _unitOfWork;

        public CatalogController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
    
        public IActionResult Restaurations(int id) {
          

            var restaurations = _unitOfWork.Restaurations.GetById(id);
            // Where(r => r.ArtworkId == id).ToListAsync();

            if (restaurations == null) {
                return NotFound();
            }
            
            return View(restaurations);
            

        }

       

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
        
        public  IActionResult Index(string artworkroom)
        {
            var artworks = _unitOfWork.Artworks.GetAll().ToList();
            
            if (!string.IsNullOrEmpty(artworkroom))
            {
                artworks = _unitOfWork.Artworks.GetArtworksByRoom(artworkroom).ToList();
            }
            var lastResaturation = new List<DateTime>();
            var restaurations = _unitOfWork.Restaurations.GetAll().ToList();
            
            foreach(var art in artworks)
            {
                var restaurationActual = new List<Restauration>();
                foreach(var rest in restaurations)
                {
                    if (art.Id == rest.ArtworkId) restaurationActual.Add(rest);
                }

                if(restaurationActual.Count == 0) 
                {
                    lastResaturation.Add(art.EntryDate);
                }
                else
                lastResaturation.Add(restaurationActual[restaurationActual.Count-1].EndDate);
                
            }
            var artworkRoomVM = new ArtworkRoomViewModel
            {
                MuseumRoom = new SelectList( _unitOfWork.Artworks.GetAll().Select(m => m.MuseumRoom).Distinct()),
                Artworks = artworks,
                LastResaturation = lastResaturation
            };

            return View(artworkRoomVM);
        }
    }
}