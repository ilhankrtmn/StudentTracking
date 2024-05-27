namespace StudentTracking.Data.EntityFramework.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int? CityID { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime? Updatedate { get; set; }
        public string Password { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int Status { get; set; }
    }
}
