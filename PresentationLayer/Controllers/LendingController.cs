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
                    System.Console.WriteLine("ID!!");
                    System.Console.WriteLine(lending.ArtworkId);
                    System.Console.WriteLine(lending.MuseumId);
                    var artwork = _unitOfWork.Artworks.GetById(lending.ArtworkId);
                    var museum = _unitOfWork.Museums.GetById(lending.MuseumId);
                    System.Console.WriteLine(artwork == null);
                    System.Console.WriteLine(museum == null);
                    lendingViewModels.Add(new LendingViewModel {
                        ArtworkId = lending.ArtworkId,
                        MuseumId = lending.MuseumId,
                        LendingToMuseum = lending,
                        ArtworkTitle = artwork.Title,
                        MuseumName = museum.Name,
                        
                    });
                
                }
                // System.Console.WriteLine(  "mmmm");
                // System.Console.WriteLine("LendingToMuseums: " + lendingToMuseums[0].ArtworkId);
                return View(lendingViewModels);
            }
            public IActionResult Edit(int artworkId, int museumId){
                // var artwork = _unitOfWork.Artworks.GetById(Id);
                System.Console.WriteLine("edITTT");
                System.Console.WriteLine(artworkId);
                System.Console.WriteLine(museumId);
                var lending = _unitOfWork.Lendings.GetById(artworkId,museumId);
                var artwork = _unitOfWork.Artworks.GetById(artworkId);
                var museum = _unitOfWork.Museums.GetById(museumId);
                System.Console.WriteLine(lending == null);
                System.Console.WriteLine(artwork == null);
                System.Console.WriteLine(museum == null);
                System.Console.WriteLine();
                var artworklendingViewModel = new ArtworklendingViewModel();
                artworklendingViewModel.ArtworkId = lending.ArtworkId;
                artworklendingViewModel.MuseumId = lending.MuseumId;
                artworklendingViewModel.Author = artwork.Author;
                artworklendingViewModel.ArtworkTitle = artwork.Title;
                // {
                
                //     Id = lending.Id,
                //     ArtworkTitle = artwork.Title,
                //     Author = artwork.Author,
                   
                // };
                return View(artworklendingViewModel);
            }
    
    }

}