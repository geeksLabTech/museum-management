using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using System.Text.Encodings.Web;
using DataLayer.Models.Auth;
using PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.UnitOfWork;

namespace museum_management.Controllers
{
    [Authorize (Roles = "Admin")]
    public class UserController : Controller 
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        

        public UserController(UserManager<IdentityUser> userManager , RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();
            foreach (var user in _userManager.Users)
            {   
                var rolList = await _userManager.GetRolesAsync(user);
                var actualUser = new User();
                actualUser.Name = user.UserName;
                actualUser.Id = user.Id;
                actualUser.Email = user.Email;
                var actualRoles = new List<string>();
                foreach (var role in rolList)
                {

                    actualRoles.Add(role + ", ");
                }
                actualUser.Role = actualRoles;


                //User actualUser = new User(int.Parse(user.Id),user.UserName,user.Email,user.PasswordHash,rolList.ToList());
                users.Add(actualUser);
            }
            return View(users);
        }
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //     var user = await _userManager.FindByIdAsync(id.ToString());
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     foreach (var user in _userManager.Users)
        //     {
        //         var userid = user.Id;
        //         if (userid == null)
        //         { 
        //             return NotFound();

        //         }
        //         else if(int.Parse(userid) == id)
        //             return View(userid);

        //     }
           
        // }
        // public async Task<IActionResult> Detail(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //     var user = await _userManager.FindByIdAsync(id.ToString());
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     foreach(var user in _userManager.Users)
        //     {
        //         var userid = user.Id;
        //         if (userid == null)
        //         {
        //             return NotFound();
        //         }
        //         else if(int.Parse(userid)==id)
        //         {
        //             var rolList = await _userManager.GetRolesAsync(user);
        //             var actualUser = new User();
        //             actualUser.Name = user.UserName;
                
        //             actualUser.Email = user.Email;
        //             actualUser.Role = rolList.ToList(); 
        //             return View(actualUser);
        //         }
        //     }
        // }

        // GET: Movies/Details/5
        public async Task<IActionResult> Delete(string? id)
        {
            var deletedBy = await _userManager.GetUserAsync(HttpContext.User);
            if(deletedBy.Id == id){
                return Unauthorized();
            }
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var deletedBy = await _userManager.GetUserAsync(HttpContext.User);
            if(deletedBy.Id == id){
                return Unauthorized();
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            
            if (user == null)
            {
                return NotFound();
            }
            
            
            var rolList = await _userManager.GetRolesAsync(user);
            DeletedUser deletedUser = new DeletedUser();
            _unitOfWork.DeletedUsers.Add(DeletedUser.Create(user, deletedBy.Email, rolList.ToList()));
            await _userManager.DeleteAsync(user);
            var check = _unitOfWork.DeletedUsers.GetAll();
            foreach(var x in check){
                System.Console.WriteLine(x.Name);
            }
            return RedirectToAction("Index", "User");
        }
    }
}