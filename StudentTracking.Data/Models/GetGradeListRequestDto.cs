namespace StudentTracking.Data.Models
{
    public class GetGradeListRequestDto
    {
        public int? UserId { get; set; }
        public int LessonId { get; set; }
    }
}