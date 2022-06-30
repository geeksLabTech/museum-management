using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using PresentationLayer.ViewModels;
using DataLayer.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using DataLayer.UnitOfWork;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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


            public IActionResult Add(int artworkId){
                CreateRestaurationViewModel createRestaurationViewModel = new CreateRestaurationViewModel();
                createRestaurationViewModel.ArtworkId = artworkId;
                createRestaurationViewModel.TypeRestauration="";
                
                return View(createRestaurationViewModel);
            }
            
            [HttpPost, ActionName("Add")]
            public IActionResult AddARestauration(int artworkId, [Bind("TypeRestauration")] CreateRestaurationViewModel createRestaurationViewModel){
                if(ModelState.IsValid){
                    Restauration restauration = new Restauration();
                    restauration.ArtworkId = artworkId;
                    System.Console.WriteLine(artworkId);
                    System.Console.WriteLine("MMMM");

                    restauration.StartDate = DateTime.Now.Date;
                    System.TimeSpan duration = new System.TimeSpan(90, 0, 0, 0); 
                    restauration.EndDate = (DateTime.Now).Add(duration);
                    restauration.RestaurationType = createRestaurationViewModel.TypeRestauration;
                    restauration.Artwork = _unitOfWork.Artworks.GetById(artworkId);
                    _unitOfWork.Restaurations.Add(restauration);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index");
                }
                var errors = ModelState.Select(x => x.Value.Errors)
                            .Where(y=>y.Count>0)
                            .ToList();

                foreach(var error in errors){
                    foreach(var e in error){
                        System.Console.WriteLine(e.ErrorMessage);
                    }
                }
                return RedirectToAction("Add", artworkId);
            }
            
            // public IActionResult Restauration()
            // {
            //     CreateRestaurationViewModel createRestaurationViewModel = new CreateRestaurationViewModel();
            //     var restaurationTypes = new List<string>();
            //     restaurationTypes.Add(RestaurationType.Complete.ToString());
            //     restaurationTypes.Add(RestaurationType.Minimal.ToString());
            //     restaurationTypes.Add(RestaurationType.Partial.ToString());
            //     createRestaurationViewModel.TypeRestauration = new SelectList(restaurationTypes);
            //     return View(createRestaurationViewModel);
            // }
    }
}