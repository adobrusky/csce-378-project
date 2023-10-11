using FitnessTracker.MVC.Helpers;
using FitnessTracker.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitnessTracker.MVC.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly ILogger<WorkoutController> _logger;
        private readonly CacheHelper _cacheHelper;

        public WorkoutController(ILogger<WorkoutController> logger, CacheHelper cacheHelper)
        {
            _logger = logger;
            _cacheHelper = cacheHelper;
        }

        public IActionResult Index()
        {
            List<WorkoutModel> lstMyWorkouts = _cacheHelper.GetMyWorkouts();
            return View(lstMyWorkouts);
        }

        public IActionResult AddWorkout()
        {
            return View();
        }

        public IActionResult AddExistingWorkout()
        {
            List<string> lstCategories = _cacheHelper.GetCategories();
            return View(lstCategories);
        }

        public IActionResult ViewWorkoutsForCategory(string category)
        {
            List<WorkoutModel> lstWorkouts = _cacheHelper.GetWorkoutsByCategory(category);
            ViewBag.Category = category;
            return View(lstWorkouts);
        }
        public IActionResult WorkoutDetails(string workoutName, string category)
        {
            WorkoutModel? oWorkout = _cacheHelper.GetWorkoutByNameAndCategory(workoutName, category);
            if (oWorkout != null)
            {
                return View(oWorkout);
            }
            return RedirectToAction("ViewWorkoutsForCategory", new { category = category });
        }

        [HttpPost]
        public IActionResult AddWorkoutToMyWorkouts(string workoutName, string category, DateTime workoutDate)
        {
            _cacheHelper.AddWorkoutToMyWorkouts(workoutName, category, workoutDate);
            return RedirectToAction("Index");
        }

        public IActionResult CreateWorkout()
        {
            ViewBag.Categories = _cacheHelper.GetCategories();
            return View(new WorkoutModel());
        }

        [HttpPost]
        public IActionResult SaveWorkout(WorkoutModel oWorkout)
        {
            _cacheHelper.AddWorkoutToExistingWorkouts(oWorkout);
            return RedirectToAction("AddWorkout");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
