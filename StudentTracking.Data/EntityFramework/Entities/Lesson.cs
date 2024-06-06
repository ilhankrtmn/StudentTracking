namespace StudentTracking.Data.EntityFramework.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}