namespace FitnessTracker.MVC.Models
{
    public class WorkoutModel
    {
        public string Name { get; set; } = String.Empty;
        public string Category { get; set; } = String.Empty;
        public DateTime? Date { get; set; }
        public List<ExerciseModel> Exercises { get; set; } = new List<ExerciseModel>();
    }

    public class ExerciseModel
    {
        public string Name { get; set; } = String.Empty;
        public string MetricToMeasure { get; set; } = String.Empty;
    }
}
