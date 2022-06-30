using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using PresentationLayer.ViewModels;
using DataLayer.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using DataLayer.UnitOfWork;
using DataLayer.Models;

namespace museum_management.Controllers{
    [Authorize (Roles = UserRoles.Restaurator)]
    public class RestaurationController: Controller{
            
            private readonly IUnitOfWork _unitOfWork;
    
            public RestaurationController(IUnitOfWork unitOfWork) {
                _unitOfWork = unitOfWork;
            }
    
            public IActionResult Index() {
                var restaurations =  _unitOfWork.Restaurations.GetAll().ToList();
                List<RestaurationViewModel> restaurationViewModels = new List<RestaurationViewModel>();
                foreach(var restauration in restaurations){
                    var artwork =  _unitOfWork.Artworks.GetById(restauration.ArtworkId);
                    restaurationViewModels.Add(new RestaurationViewModel{
                        ArtworkTitle = restauration.Artwork.Title,
                        Restauration = restauration
                    });
                }

                return View(restaurationViewModels);
            }
            
            
            public IActionResult Submit( int artworkid,[Bind("Type , EndDate" )] CreatterestaurationViewModel creatterestaurationViewModel) {

                Restauration restauration = new Restauration
                {
                    StartDate = DateTime.Now,
                    EndDate = creatterestaurationViewModel.EndDate,
                    ArtworkId = artworkid,
                    Artwork = _unitOfWork.Artworks.GetById(artworkid),
                };
                return View("Index","Restauration");
    
            }
            
            public IActionResult Restauration()
            {
                return View();
            }
    }
}