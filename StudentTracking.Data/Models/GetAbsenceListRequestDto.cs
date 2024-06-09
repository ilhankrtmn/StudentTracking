namespace StudentTracking.Data.Models
{
    public class GetAbsenceListRequestDto
    {
        public int? UserId { get; set; }
        public int LessonId { get; set; }
    }
}