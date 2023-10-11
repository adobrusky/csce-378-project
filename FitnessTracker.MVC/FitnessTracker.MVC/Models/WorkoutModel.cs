namespace FitnessTracker.MVC.Models
{
    public class WorkoutModel
    {
        public string Name { get; set; } = String.Empty;
        public string Category { get; set; } = String.Empty;
        public DateTime? Date { get; set; }
        public List<ExerciseModel> Exercises { get; set; } = new List<ExerciseModel>();

        public WorkoutModel Clone()
        {
            var clonedWorkout = new WorkoutModel
            {
                Name = this.Name,
                Category = this.Category,
                Date = this.Date
            };
            foreach (var exercise in this.Exercises)
            {
                clonedWorkout.Exercises.Add(exercise.Clone());
            }
            return clonedWorkout;
        }
    }

    public class ExerciseModel
    {
        public string Name { get; set; } = String.Empty;
        public string MetricToMeasure { get; set; } = String.Empty;

        public ExerciseModel Clone()
        {
            return new ExerciseModel
            {
                Name = this.Name,
                MetricToMeasure = this.MetricToMeasure
            };
        }
    }
}
