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
        [HttpPost]
        public IActionResult Index(Artwork artwork)
        {
            return View();
        }
       

}
}