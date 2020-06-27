using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ManagmentPortal_0_3.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(string Id)
        {
            return View("Erorr404");
        }
    }
}