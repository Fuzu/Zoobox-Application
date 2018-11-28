////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Controllers\HomeController.cs
//
// summary:	Implements the home controller class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZooboxApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace ZooboxApplication.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Controlador de páginas Home. </summary>
    ///
    /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class HomeController : Controller
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Página Inicial. </summary>
        ///
        /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
        ///
        /// <returns>   Retorna a view da página se o utilizador estiver logado. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the about. </summary>
        ///
        /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the contact. </summary>
        ///
        /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the privacy. </summary>
        ///
        /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult Privacy()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the error. </summary>
        ///
        /// <remarks>   Diogo Paulino, 28/11/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
