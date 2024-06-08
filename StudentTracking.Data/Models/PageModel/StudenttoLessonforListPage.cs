namespace StudentTracking.Data.Models.PageModel
{
    public class StudenttoLessonforListPage
    {
        public List<StudenttoLesson> StudenttoLessons { get; set; }
        public int LessonId { get; set; }
    }

    public class StudenttoLesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        // TODO: öğrenci Numarası ekle
        public bool Status { get; set; }
    }
}