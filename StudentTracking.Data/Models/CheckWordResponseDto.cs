using StudentTracking.Data.Enums;

namespace StudentTracking.Data.Models
{
    public class CheckWordResponseDto
    {
        public List<CheckWord> CheckWords { get; set; }
        public int WrongCount { get; set; }
    }

    public class CheckWord
    {
        public char CorrectLetter { get; set; }
        public char? UserLetter { get; set; }
        public GameParticipateCorrectEnum? IsCorrect { get; set; }
    }
}
