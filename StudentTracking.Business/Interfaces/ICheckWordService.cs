using StudentTracking.Data.Models;

namespace StudentTracking.Business.Interfaces
{
    public interface ICheckWordService
    {
        CheckWordResponseDto CheckWord(string correctWord, string userWord);
        CheckWordResponseDto OneToOneCheckWord(string correctWord, string userWord);
        CheckWordResponseDto ReverseCheckWord(string correctWord, string userWord);
        CheckWordResponseDto CheckWordFromStart(string correctWord, string userWord);
        CheckWordResponseDto SearchInCheckWord(string correctWord, string userWord);
    }
}
