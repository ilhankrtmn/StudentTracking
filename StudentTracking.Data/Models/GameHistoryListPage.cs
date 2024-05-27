namespace StudentTracking.Data.Models
{
    public class GameHistoryListPage
    {
        public List<GameHistoryResponseDto> GameHistoryResponseDtos { get; set; }
    }

    public class GameHistoryResponseDto
    {
        public CheckWordResponseDto CheckWordResponseDtos { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
