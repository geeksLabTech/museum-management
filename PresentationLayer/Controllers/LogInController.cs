using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using System.Text.Encodings.Web;
using DataLayer.Models;
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