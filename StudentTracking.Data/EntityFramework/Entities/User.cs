namespace StudentTracking.Data.EntityFramework.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? StudentNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ChildrenId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}