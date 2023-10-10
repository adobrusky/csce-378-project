namespace FitnessTracker.MVC.Models
{
    public class WorkoutModel
    {
        string Name { get; set; } = String.Empty;
        string Category { get; set; } = String.Empty;
        DateTime? Date { get; set; }
        List<ExerciseModel> Exercises { get; set; } = new List<ExerciseModel>();
    }

    public class ExerciseModel
    {
        string Name { get; set; } = String.Empty;
        string MetricToMeasure { get; set; } = String.Empty;
    }
}
