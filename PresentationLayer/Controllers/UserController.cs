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
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            System.Console.WriteLine("usuario nulo en get **********");
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            
            var user = await _userManager.FindByIdAsync(id.ToString());
            System.Console.WriteLine("**************************");
            System.Console.WriteLine("**************************");
            if (user == null)
            {
                return NotFound();
            }
            System.Console.WriteLine("mura user pa borrar");
            System.Console.WriteLine(user.Email);
            await _userManager.DeleteAsync(user);
            
            return RedirectToAction("Index", "User");
        }
    }
}