using FitnessTracker.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitnessTracker.MVC.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly ILogger<WorkoutController> _logger;

        public WorkoutController(ILogger<WorkoutController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
