
using DataLayer.Models;
using DataLayer.Models.Auth;
using DataLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.ViewModels;

namespace museum_management.Controllers {

    [Authorize(Roles = UserRoles.Director)]
    public class BorrowArtworksController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        public BorrowArtworksController(IUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string museumName) {
            var artworks = _unitOfWork.Artworks.GetAllArtworksOfOtherMuseums();
            System.Console.WriteLine(artworks==null);
            if (!string.IsNullOrEmpty(museumName))
            {   
                int museumId = _unitOfWork.Museums.GetMuseumIdByName(museumName);
                artworks = _unitOfWork.Artworks.GetArtworksByMuseumId(museumId);
            }
            var lastResaturation = new List<DateTime>();
            var restaurations = _unitOfWork.Restaurations.GetAll().ToList();
            System.Console.WriteLine("imprimir obras");
            System.Console.WriteLine( artworks.First());
            foreach(var x in artworks){
                System.Console.WriteLine("titulo");
                System.Console.WriteLine(x.Title);
            }
            
            var artworkRoomVM = new ArtworkRoomViewModel
            {
                MuseumRoom = new SelectList( _unitOfWork.Museums.GetAll().Select(m => m.Name).Distinct()),
                Artworks = artworks.ToList(),
                LastResaturation = lastResaturation
            };

            return View(artworkRoomVM);
            
        }

        public IActionResult Borrow(int id) {
            var artwork = _unitOfWork.Artworks.GetById(id);
            artwork.ActualMuseumId = 3;
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        
        // public IActionResult BorrowConfirm(int artworkId){
        //     var artwork = _unitOfWork.Artworks.GetById(artworkId);
        //     artwork.OriginMuseumId = 3;
        //     // _unitOfWork.Lendings.Update(lending);
        //     _unitOfWork.Complete();
        //     return RedirectToAction("Index");
        // }

            
        // public IActionResult Reject(int artworkId, int museumId){
        //     var lending = _unitOfWork.Lendings.GetById(artworkId,museumId);
        //     lending.LendingState = LendingState.Denied;
        //     _unitOfWork.Complete();
        //     return RedirectToAction("Index");
                

        // }
    }
}
