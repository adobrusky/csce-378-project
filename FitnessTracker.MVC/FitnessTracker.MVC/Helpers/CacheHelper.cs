using FitnessTracker.MVC.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FitnessTracker.MVC.Helpers
{
    public class CacheHelper
    {
        private readonly IMemoryCache _cache;

        // Define a cache key
        public string ExistingWorkoutsCacheKey(string sCategory) => $"ExistingWorkouts_{sCategory}";
        public const string CategoriesCacheKey = $"WorkoutCategories";
        public const string MyWorkoutsCacheKey = $"MyWorkouts";

        public CacheHelper(IMemoryCache cache)
        {
            _cache = cache;

            // Initialize categories cache if not exists
            if (!_cache.TryGetValue<List<string>>(CategoriesCacheKey, out _))
            {
                _cache.Set(CategoriesCacheKey, new List<string>() { "CARDIO", "MEDITATION", "STRENGTH", "SPORTS", "OTHER" });
            }

            // Initialize existing workouts cache if not exists
            foreach (string sCategory in _cache.Get<List<string>>(CategoriesCacheKey))
            {
                if (!_cache.TryGetValue<List<WorkoutModel>>(ExistingWorkoutsCacheKey(sCategory), out _))
                {
                    List<WorkoutModel> lstWorkouts = new();
                    switch (sCategory)
                    {
                        case "CARDIO":
                            lstWorkouts.Add(new WorkoutModel
                            {
                                Name = "Morning Run",
                                Category = sCategory,
                                Exercises = new List<ExerciseModel>
                                {
                                new ExerciseModel { Name = "Running", MetricToMeasure = "2 miles" },
                                new ExerciseModel { Name = "Jumping Jacks", MetricToMeasure = "20 repetitions" }
                            }
                            });
                            lstWorkouts.Add(new WorkoutModel
                            {
                                Name = "HIIT Session",
                                Category = sCategory,
                                Exercises = new List<ExerciseModel>
                                {
                                new ExerciseModel { Name = "Burpees", MetricToMeasure = "15 repetitions" },
                                new ExerciseModel { Name = "Mountain Climbers", MetricToMeasure = "30 repetitions" }
                            }
                            });
                            break;

                        case "MEDITATION":
                            lstWorkouts.Add(new WorkoutModel
                            {
                                Name = "Morning Meditation",
                                Category = sCategory,
                                Exercises = new List<ExerciseModel>
                                {
                                new ExerciseModel { Name = "Breathing Exercise", MetricToMeasure = "10 minutes" },
                                new ExerciseModel { Name = "Visualization", MetricToMeasure = "15 minutes" }
                            }
                            });
                            break;

                        case "STRENGTH":
                            lstWorkouts.Add(new WorkoutModel
                            {
                                Name = "Gym Session",
                                Category = sCategory,
                                Exercises = new List<ExerciseModel>
                                {
                                new ExerciseModel { Name = "Barbell Bench Press", MetricToMeasure = "3 sets 8 repetitions" },
                                new ExerciseModel { Name = "Squats", MetricToMeasure = "3 sets 8 repetitions" },
                                new ExerciseModel { Name = "Deadlift", MetricToMeasure = "3 sets 8 repetitions" }
                            }
                            });
                            break;

                        case "SPORTS":
                            lstWorkouts.Add(new WorkoutModel
                            {
                                Name = "Basketball Practice",
                                Category = sCategory,
                                Exercises = new List<ExerciseModel>
                                {
                                new ExerciseModel { Name = "Free Throws", MetricToMeasure = "20 shots" },
                                new ExerciseModel { Name = "Dribbling Drills", MetricToMeasure = "15 minutes" }
                            }
                            });
                            break;

                        case "OTHER":
                            lstWorkouts.Add(new WorkoutModel
                            {
                                Name = "Mixed Routine",
                                Category = sCategory,
                                Exercises = new List<ExerciseModel>
                                {
                                new ExerciseModel { Name = "Jump Rope", MetricToMeasure = "10 minutes" },
                                new ExerciseModel { Name = "Plank", MetricToMeasure = "3 minutes" }
                            }
                            });
                            break;
                    }

                    _cache.Set(ExistingWorkoutsCacheKey(sCategory), lstWorkouts);
                }
            }

            // Initialize my workouts cache if not exists
            if (!_cache.TryGetValue<List<WorkoutModel>>(MyWorkoutsCacheKey, out _))
            {
                // currently the default for my workouts is just the existing workouts in the first category
                List<WorkoutModel> lstInitialMyWorkouts = GetWorkoutsByCategory(GetCategories().FirstOrDefault());

                // Set the dates for each workout
                DateTime currentDate = DateTime.Today;
                foreach (var workout in lstInitialMyWorkouts)
                {
                    workout.Date = currentDate;
                    currentDate = currentDate.AddDays(1);
                }

                _cache.Set(MyWorkoutsCacheKey, lstInitialMyWorkouts);
            }
        }

        public List<WorkoutModel> GetWorkoutsByCategory(string sCategory)
        {
            return _cache.Get<List<WorkoutModel>>(ExistingWorkoutsCacheKey(sCategory));
        }

        public List<string> GetCategories()
        {
            return _cache.Get<List<string>>(CategoriesCacheKey);
        }

        public List<WorkoutModel> GetMyWorkouts()
        {
            return _cache.Get<List<WorkoutModel>>(MyWorkoutsCacheKey);
        }

        public void AddWorkoutToExistingWorkouts(WorkoutModel oWorkout)
        {
            List<WorkoutModel> lstWorkouts = GetWorkoutsByCategory(oWorkout.Category);
            lstWorkouts.Add(oWorkout);
            _cache.Set(ExistingWorkoutsCacheKey(oWorkout.Category), lstWorkouts);
        }

        public WorkoutModel? GetWorkoutByNameAndCategory(string workoutName, string category)
        {
            List<WorkoutModel> workouts = GetWorkoutsByCategory(category);
            return workouts.FirstOrDefault(w => w.Name == workoutName);
        }

        public void AddWorkoutToMyWorkouts(string workoutName, string category, DateTime workoutDate)
        {
            WorkoutModel oWorkout = GetWorkoutByNameAndCategory(workoutName, category);
            if (oWorkout != null)
            {
                oWorkout.Date = workoutDate;
                List<WorkoutModel> lstWorkouts = GetMyWorkouts();
                lstWorkouts.Add(oWorkout);
                _cache.Set(MyWorkoutsCacheKey, lstWorkouts);
            }
        }
    }

}
