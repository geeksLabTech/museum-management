using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using PresentationLayer.ViewModels;
using DataLayer.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLayer;
using DataLayer.UnitOfWork;
using DataLayer.Models;

namespace museum_management.Controllers{
    [Authorize(Roles = UserRoles.Director)]
    public class LendingController: Controller{
            
            private readonly IUnitOfWork _unitOfWork;
    
            public LendingController( IUnitOfWork unitOfWork) {
                _unitOfWork = unitOfWork;
            }
    
            public IActionResult Index(string? currentLendingState) {
                System.Console.WriteLine("SearchState");
                System.Console.WriteLine(currentLendingState);
                IEnumerable<LendingToMuseum> lendingToMuseums;
                List<LendingViewModel> lendingViewModels = new List<LendingViewModel>();

                if(currentLendingState != null) {
                    lendingToMuseums = _unitOfWork.Lendings.GetLendingsByState(currentLendingState);
                }
                else{
                    lendingToMuseums = _unitOfWork.Lendings.GetAll();
                }
                foreach (var lending in lendingToMuseums) {
                    var artwork = _unitOfWork.Artworks.GetById(lending.ArtworkId);
                    var museum = _unitOfWork.Museums.GetById(lending.MuseumId);
                    lendingViewModels.Add(new LendingViewModel {
                        
                        ArtworkId = lending.ArtworkId,
                        MuseumId = lending.MuseumId,
                        LendingToMuseum = lending,
                        ArtworkTitle = artwork.Title,
                        MuseumName = museum.Name,
                        
                        
                    });
                }
                    // Create LendingTableViewModel
                LendingTableViewModel lendingTableViewModel = new LendingTableViewModel
                {
                    CurrentLendingState=currentLendingState==null ? "" : currentLendingState,
                    LendingStateList = new SelectList(_unitOfWork.Lendings.GetAll().Select(m => m.LendingState).Distinct()),
                    LendingViewModels = lendingViewModels
                    
                };
                   
                // System.Console.WriteLine(  "mmmm");
                // System.Console.WriteLine("LendingToMuseums: " + lendingToMuseums[0].ArtworkId);
                return View(lendingTableViewModel);
            }


            public IActionResult Edit(int artworkId, int museumId){
                var lending = _unitOfWork.Lendings.GetById(artworkId,museumId);
                var artwork = _unitOfWork.Artworks.GetById(artworkId);
                var museum = _unitOfWork.Museums.GetById(museumId);
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

            public IActionResult Accept(int artworkId, int museumId){
                var lending = _unitOfWork.Lendings.GetById(artworkId,museumId);
                lending.LendingState = LendingState.Lended;
                // _unitOfWork.Lendings.Update(lending);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            public IActionResult Reject(int artworkId, int museumId){
                var lending = _unitOfWork.Lendings.GetById(artworkId,museumId);
                lending.LendingState = LendingState.Denied;
                _unitOfWork.Complete();
                return RedirectToAction("Index");
                

            }
    
    }

}