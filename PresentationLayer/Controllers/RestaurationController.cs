using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using PresentationLayer.ViewModels;
using DataLayer.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using DataLayer.UnitOfWork;
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
            
            public IActionResult Restaurations(int id) {
                
    
                var restaurations = _unitOfWork.Restaurations.GetById(id);
    
                if (restaurations == null) {
                    return NotFound();
                }
                else
                restaurations.StartDate = DateTime.Now;
                return View(restaurations);
    
            }
            
    }
}