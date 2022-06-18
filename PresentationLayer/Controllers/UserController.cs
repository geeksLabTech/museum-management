using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using System.Text.Encodings.Web;
using DataLayer.Models;
using PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace museum_management.Controllers
{
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
    }
}