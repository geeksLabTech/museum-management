using DataLayer.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using PresentationLayer.ViewModels;
//using Microsoft.AspNetCore.Identity.IdentityModel;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using PresentationLayer.Globals;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace museum_management.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index (){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var identity = new ClaimsIdentity(authClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity); 

                var roles = await _userManager.GetRolesAsync(user);
                foreach(var rol in roles){
                    System.Console.WriteLine("checkea");
                    System.Console.WriteLine(rol);
                }
               var token = GetToken(authClaims);
               var print_token = new JwtSecurityTokenHandler().WriteToken(token);
               System.Console.WriteLine(print_token);
                SavedJwt.Jwt = token.ToString();
                System.Console.WriteLine("mira el token");
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                System.Console.WriteLine("******************");
                System.Console.WriteLine(identity.IsAuthenticated);
                System.Console.WriteLine("******************");
                SavedJwt.IsAuthenticated = identity.IsAuthenticated;
            //    HttpContext.Request.Headers.Add("Authorization", "Bearer " + token);
                // HttpContext.Response.Cookies.Append("jwt", token.ToString(), new CookieOptions { HttpOnly = true, Secure = true }); 
                return RedirectToAction("Index", "Catalog");
                // return Ok(new
                // {
                //     token = new JwtSecurityTokenHandler().WriteToken(token),
                //     expiration = token.ValidTo
                // });
            }
            return View();
        }

        public async Task<IActionResult> Register(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterInput([FromForm] RegisterViewModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
             
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromForm] RegisterViewModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Director))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Director));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Director))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.CatalogManager));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Director))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Restaurator));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Director);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.CatalogManager);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Restaurator);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}