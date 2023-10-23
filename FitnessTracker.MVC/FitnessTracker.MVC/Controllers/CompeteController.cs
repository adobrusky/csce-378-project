using FitnessTracker.MVC.Helpers;
using FitnessTracker.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitnessTracker.MVC.Controllers
{
    public class CompeteController : Controller
    {
        private readonly ILogger<CompeteController> _logger;

        public CompeteController(ILogger<CompeteController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SoccerLovers()
        {
            return View();
        }

        public IActionResult WeightLifters()
        {
            return View();

        }

        public IActionResult OutdoorRunners()
        {
            return View();
        }
        public IActionResult SLGroupStatistics()
        {
            return View();
        }
        public IActionResult wLGroupStatistics()
        {
            return View();
        }
        public IActionResult ORGroupStatistics()
        {
            return View();
        }

        public IActionResult SLSettings()
        {
            return View();
        }

        public IActionResult ORSettings()
        {
            return View();
        }

        public IActionResult WLSettings()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveSoccerLoversSettings()
        {
            TempData["SuccessMessage"] = "Settings successfully saved!";
            return RedirectToAction("SoccerLovers");
        }

        [HttpPost]
        public IActionResult SaveWeightLiftersSettings()
        {
            TempData["SuccessMessage"] = "Settings successfully saved!";
            return RedirectToAction("WeightLifters");
        }

        [HttpPost] 
        public IActionResult SaveOutdoorRunnersSettings()
        {
            TempData["SuccessMessage"] = "Settings successfully saved!";
            return RedirectToAction("OutdoorRunners");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
