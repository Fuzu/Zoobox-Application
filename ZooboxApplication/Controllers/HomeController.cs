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
using ZooboxApplication.Data;

using Microsoft.EntityFrameworkCore;
using Stripe;
using ZooboxApplication.Models.Donations;

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
        ///
        public UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            //ApplicationSeed seed = new ApplicationSeed(context, userManager, roleManager);
            //seed.Users().Wait();
            //seed.Animals().Wait();
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            // GET: Jobs
          
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var Jobs = _context.Job.Include(j => j.ApplicationUser).Where(s => s.State.Equals("Activo"));
            var Payments = _context.Donation.Include(j => j.ApplicationUser);
            var DonationAmountMoney = _context.Donation.Include(j => j.ApplicationUser).Where(s => s.DonationType.Equals(1)).Where(s => s.Status.Equals("success"));
            var DonationsAmountKG = _context.Donation.Include(j => j.ApplicationUser).Where(s => s.DonationType.Equals(2002));
            var Follow = _context.Sponsorship.Where(j => j.UserId == user.Id).Where(s => s.Status.Equals("Seguir"));
            double sumDonations = 0;
            if (DonationAmountMoney != null)
            {
                foreach (Donation item in DonationAmountMoney)
                {
                    sumDonations += int.Parse(item.Quantity);
                }
                double sumDonationsKG = 0;
                foreach (Donation item in DonationsAmountKG)
                {
                    sumDonationsKG += int.Parse(item.Quantity);
                }

                var animals = from m in _context.Animal
                              select m;
                ViewBag.AmountAnimals = animals.Count();

                var users = from m in _context.Users
                            select m;
                ViewBag.AmountUsers = users.Count();
                ViewBag.DonationAmount = sumDonations;
                ViewBag.sumDonationsKG = sumDonationsKG;
            }

            var model = new List<Object>
            {
                 await Jobs.ToListAsync(),
                 await Payments.ToListAsync(),
                 await Follow.ToListAsync(),
            };
            
     
            return View(model);
        }

        public class PaymentModel
        {
            public string stripeTokenType { get; set; }
            public string stripeToken { get; set; }
            public string stripeEmail { get; set; }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Payment(int id, [Bind("stripeTokenType, stripeToken, stripeEmail")]PaymentModel model)
        {
            // Set your secret key: remember to change this to your live secret key in production
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.SetApiKey("sk_test_tjnJtycE4g8eavRpdGYvQiHc");

            var donation = _context.Donation.Find(id);

            // Token is created using Checkout or Elements!
            // Get the payment token submitted by the form:
            var token = (string) model.stripeToken; // Using ASP.NET MVC

            var options = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(donation.Quantity+"00"),
                Currency = "eur",
                Description = donation.Description,
                SourceId = token,
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            if(charge.Status == "succeeded")
            {
                donation.Status = "success";
            }
            else
            {
                donation.Status = "reject";
            }
            _context.Donation.Update(donation);
            _context.SaveChangesAsync();
            return Redirect("/");
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
