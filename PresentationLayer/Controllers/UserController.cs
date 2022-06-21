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

namespace museum_management.Controllers
{
    [Authorize (Roles = "Admin")]
    public class UserController : Controller 
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager , RoleManager<IdentityRole> roleManager) 
        {
           _userManager = userManager;
           _roleManager = roleManager;   
        }
        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();
            foreach (var user in _userManager.Users)
            {   
                var rolList = await _userManager.GetRolesAsync(user);
                var actualUser = new User();
                actualUser.Name = user.UserName;
                
                actualUser.Email = user.Email;
                actualUser.Role = rolList.ToList();

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
        [HttpPost]
        public async Task<IActionResult> Delete(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(user);
            
            return RedirectToAction("Index");
        }
    }
}