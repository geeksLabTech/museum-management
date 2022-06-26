using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using PresentationLayer.ViewModels;
using DataLayer.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using DataLayer;
using DataLayer.UnitOfWork;
namespace museum_management.Controllers{
    [Authorize(Roles = UserRoles.Director)]
    public class LendingController: Controller{
            
            private readonly IUnitOfWork _unitOfWork;
    
            public LendingController( IUnitOfWork unitOfWork) {
                _unitOfWork = unitOfWork;
            }
    
            public IActionResult Index() {
                
                var lendingToMuseums =  _unitOfWork.Lendings.GetAll();
                List<LendingViewModel> lendingViewModels = new List<LendingViewModel>();

                foreach (var lending in lendingToMuseums) {
                    System.Console.WriteLine(lending.Id);
                    System.Console.WriteLine(lending.ArtworkId);
                    System.Console.WriteLine(lending.MuseumId);
                    var artwork = _unitOfWork.Artworks.GetById(lending.ArtworkId);
                    var museum = _unitOfWork.Museums.GetById(lending.MuseumId);
                    lendingViewModels.Add(new LendingViewModel {
                        Id = artwork.Id,
                        LendingToMuseum = lending,
                        ArtworkTitle = artwork.Title,
                        MuseumName = museum.Name,
                        
                    });
                
                }
                // System.Console.WriteLine(  "mmmm");
                // System.Console.WriteLine("LendingToMuseums: " + lendingToMuseums[0].ArtworkId);
                return View(lendingViewModels);
            }
            public IActionResult Edit(int Id){
                var artwork = _unitOfWork.Artworks.GetById(Id);
                var lending = _unitOfWork.Lendings.GetAll();
                var artworklendingViewModel = new ArtworklendingViewModel 
                {
                    Id = artwork.Id,
                    ArtworkTitle = artwork.Title,
                    Athor = artwork.Author,
                   
                };
                return View(artworklendingViewModel);
            }
    
    }

}