namespace StudentTracking.Data.EntityFramework.Entities
{
    public class UserEmailOtp
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Pincode { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
