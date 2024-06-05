namespace StudentTracking.Data.Models
{
    public class ResetPasswordRequestDto
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
