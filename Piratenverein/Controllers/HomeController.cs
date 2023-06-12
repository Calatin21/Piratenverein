using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Piratenverein.Models;

namespace Piratenverein.Controllers {
    public class HomeController : Controller {      
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }
        public IActionResult Index() {
            return View();
        }
        public IActionResult Privacy() {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pirat pirat) {
            string ergebnis;
            if (ModelState.IsValid) {
                if (pirat.Jahresalter < 10) {
                    ergebnis = "PiratJuniors";
                   
                    return RedirectToAction("CreateNew", ergebnis, pirat);
                }
                else {
                    ergebnis = "Pirats";
                    
                    return RedirectToAction("CreateNew", ergebnis, pirat);
                }
            }
            return RedirectToAction("Create", pirat);
        }     
    }
}