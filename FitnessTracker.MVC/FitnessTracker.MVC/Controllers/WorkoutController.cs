using FitnessTracker.MVC.Helpers;
using FitnessTracker.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
            List<WorkoutModel> allWorkouts = _cacheHelper.GetMyWorkouts();
            var distinctDates = allWorkouts.Where(w => w.Date.HasValue).Select(x => x.Date.Value).Distinct().OrderBy(d => d).ToList();
            return View(distinctDates);
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
            TempData["SuccessMessage"] = "Workout successfully saved to my workouts!";
            return RedirectToAction("Index");
        }

        public IActionResult CreateWorkout()
        {
            ViewBag.Categories = _cacheHelper.GetCategories();
            return View(new WorkoutModel());
        }

        [HttpPost]
        public IActionResult RemoveWorkoutFromMyWorkouts(string workoutName, string category, DateTime dDate)
        {
            _cacheHelper.RemoveWorkoutFromMyWorkouts(workoutName, category, dDate);
            TempData["SuccessMessage"] = workoutName + " has been successfully removed from " + dDate.ToString("dddd, MMMM dd") + "!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveWorkout(WorkoutModel oWorkout)
        {
            _cacheHelper.AddWorkoutToExistingWorkouts(oWorkout);
            TempData["SuccessMessage"] = "Workout successfully saved to existing workouts!";
            return RedirectToAction("AddWorkout");
        }

        public IActionResult WorkoutsByDate(DateTime date)
        {
            List<WorkoutModel> lstWorkoutsByDate = _cacheHelper.GetMyWorkoutsByDate(date);
            ViewBag.Date = date;
            return View(lstWorkoutsByDate);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult FilterWorkouts()
        {
            ViewBag.Categories = _cacheHelper.GetCategories();
            return View();
        }

        [HttpPost]
        public IActionResult ApplyFilters(string category, DateTime date1, DateTime date2)
        {
            _cacheHelper.ApplyFilters(category, date1, date2);
            TempData["SuccessMessage"] = "Filters applied";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearFilters()
        {
            _cacheHelper.ClearFilters();
            TempData["SuccessMessage"] = "Filters cleared";
            return RedirectToAction("Index");
        }
    }
}
