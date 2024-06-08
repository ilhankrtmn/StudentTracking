namespace StudentTracking.Data.Models.PageModel
{
    public class StudentLessonListforListPage
    {
        public List<MergedData> MergedDataList { get; set; }
    }
    public class MergedData
    {
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public int? MidtermGrade { get; set; }
        public int? FinalGrade { get; set; }
        public int? AbsenceCount { get; set; }
    }
}