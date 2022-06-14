using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using museum_management.Data;
using System.Text.Encodings.Web;
using museum_management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace museum_management.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}