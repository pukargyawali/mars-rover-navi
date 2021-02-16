using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoverWebApp.Controllers
{
    public class RoverDocumentationController : Controller
    {
        // GET: /<controller>/
        /// <summary>
        /// This return the page for the documentation
        /// of the application
        /// </summary>
        /// <returns></returns>
        public IActionResult RoverDocumentation()
        {
            return View();
        }

        /// <summary>
        /// Over View of the Problem and
        /// Expected output
        /// </summary>
        /// <returns></returns>
        public IActionResult OverView()
        {
            return View();
        }
    }
}
