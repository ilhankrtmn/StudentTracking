namespace StudentTracking.Data.EntityFramework.Entities
{
    public class Absence
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public int Count { get; set; }
        public User User { get; set; }
        public Lesson Lesson { get; set; }
    }
}