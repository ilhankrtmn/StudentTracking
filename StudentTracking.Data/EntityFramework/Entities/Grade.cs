namespace StudentTracking.Data.EntityFramework.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public int MidtermGrade { get; set; }
        public int FinalGrade { get; set; }
        public User User { get; set; }
        public Lesson Lesson { get; set; }
    }
}