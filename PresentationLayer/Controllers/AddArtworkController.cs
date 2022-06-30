using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using System.Text.Encodings.Web;
using DataLayer.Models;
using PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using DataLayer.Models.Auth;

using DataLayer.UnitOfWork;
namespace museum_management.Controllers{
    
    [Authorize (Roles = UserRoles.CatalogManager)]
    public class AddArtworkController : Controller
    {
        private readonly IUnitOfWork _uniOfWork;

        public AddArtworkController (IUnitOfWork unitOfWork){
            _uniOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index(){
    
            return View();
        }
        [HttpPost, ActionName("Index")]
        public IActionResult AddArtwork([Bind("Title,Author,EconomicValue,Period,CreationDate,MuseumRoom")] AddArtworkViewModel addArtworkViewModel){
            if(ModelState.IsValid){
                Artwork artwork = new Artwork();
                artwork.Title = addArtworkViewModel.Title;
                artwork.Author = addArtworkViewModel.Author;
                artwork.EconomicValue = addArtworkViewModel.EconomicValue;
                artwork.Period = addArtworkViewModel.Period;
                artwork.CreationDate = addArtworkViewModel.CreationDate;
                artwork.EntryDate = DateTime.Now.Date;
                artwork.MuseumRoom = addArtworkViewModel.MuseumRoom;
                artwork.LendingToMuseums = new List<LendingToMuseum>();
                artwork.Restaurations = new List<Restauration>();
                System.Console.WriteLine("datetime");
                System.Console.WriteLine(artwork.EntryDate);
                _uniOfWork.Artworks.Add(artwork);
                _uniOfWork.Complete();
                return RedirectToAction("Index", "Catalog");
            }
            var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();

            foreach(var error in errors){
                foreach(var e in error){
                    System.Console.WriteLine(e.ErrorMessage);
                }
            }
            return View(addArtworkViewModel);
        }
        
       

}
}