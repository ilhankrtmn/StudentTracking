namespace StudentTracking.Data.EntityFramework.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}