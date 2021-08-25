using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using ResnytskyiLawyer.Models;

namespace ResnytskyiLawyer.Controllers
{
    public class LawyerInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}