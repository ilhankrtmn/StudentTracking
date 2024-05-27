using StudentTracking.Business.Interfaces;
using StudentTracking.Data.Enums;
using StudentTracking.Data.Models;

namespace StudentTracking.Business.Services
{
    public class CheckWordService : ICheckWordService
    {
        public CheckWordResponseDto CheckWord(string correctWord, string userWord)
        {            
            var result1 = OneToOneCheckWord(correctWord, userWord);
            var result2 = ReverseCheckWord(correctWord, userWord);
            var result3 = CheckWordFromStart(correctWord, userWord);
            var result4 = SearchInCheckWord(correctWord, userWord);

            List<CheckWordResponseDto> results = new List<CheckWordResponseDto>();
            results.Add(result1);
            results.Add(result2);
            results.Add(result3);
            results.Add(result4);

            CheckWordResponseDto minimumResult = results.Where(p => p.CheckWords != null).OrderBy(r => r.WrongCount).FirstOrDefault();

            return minimumResult;
        }

        public CheckWordResponseDto OneToOneCheckWord(string correctWord, string userWord)
        {
            int correctLength = correctWord.Length;
            int userLength = userWord.Length;

            if (correctLength != userLength)
            {
                return new CheckWordResponseDto();
            }

            int wrongCount = 0;
            List<CheckWord> checkWords = new List<CheckWord>();

            for (int i = 0; i < correctLength; i++)
            {
                checkWords.Add(new CheckWord
                {
                    CorrectLetter = char.ToLower(correctWord[i]),
                    UserLetter = char.ToLower(userWord[i]),
                    IsCorrect = char.ToLower(correctWord[i]) == char.ToLower(userWord[i]) ? GameParticipateCorrectEnum.Doğru : GameParticipateCorrectEnum.Yanlış
                });

                if (!(char.ToLower(correctWord[i]) == char.ToLower(userWord[i])))
                {
                    wrongCount++;
                }
            }

            return new CheckWordResponseDto
            {
                CheckWords = checkWords,
                WrongCount = wrongCount
            };
        }

        public CheckWordResponseDto ReverseCheckWord(string correctWord, string userWord)
        {
            int correctLength = correctWord.Length;
            int userLength = userWord.Length;

            int wrongCount = 0;
            List<CheckWord> checkWords = new List<CheckWord>();
            int k = 1;
            int count = (userLength < correctLength) ? userLength : correctLength;

            for (int i = 0; i < count; i++)
            {
                char correctLetter = char.ToLower(correctWord[correctLength - k]);
                char? userLetter = (userLength >= k) ? char.ToLower(userWord[userLength - k]) : null;
                var isCorrect = correctLetter == userLetter;

                checkWords.Add(new CheckWord
                {
                    CorrectLetter = correctLetter,
                    UserLetter = userLetter,
                    IsCorrect = (isCorrect == true) ? GameParticipateCorrectEnum.Doğru : GameParticipateCorrectEnum.Yanlış
                });

                if (!isCorrect)
                {
                    wrongCount++;
                }

                k++;
            }

            if (correctLength > userLength)
            {
                for (int i = correctLength - userLength - 1; i >= 0; i--)
                {
                    char correctLetter = char.ToLower(correctWord[i]);
                    checkWords.Add(new CheckWord
                    {
                        CorrectLetter = correctLetter,
                        UserLetter = '?',
                        IsCorrect = GameParticipateCorrectEnum.Boş
                    });
                    wrongCount++;
                }
            }
            else
            {
                for (int i = userLength - correctLength - 1; i >= 0; i--)
                {
                    char userLetter = char.ToLower(userWord[i]);
                    checkWords.Add(new CheckWord
                    {
                        CorrectLetter = '?',
                        UserLetter = userLetter,
                        IsCorrect = GameParticipateCorrectEnum.Boş
                    });
                    wrongCount++;
                }
            }

            checkWords.Reverse();

            return new CheckWordResponseDto
            {
                CheckWords = checkWords,
                WrongCount = wrongCount
            };
        }

        public CheckWordResponseDto CheckWordFromStart(string correctWord, string userWord)
        {
            int correctLength = correctWord.Length;
            int userLength = userWord.Length;

            int wrongCount = 0;
            List<CheckWord> checkWords = new List<CheckWord>();
            int count = (userLength < correctLength) ? userLength : correctLength;

            for (int i = 0; i < count; i++)
            {
                char correctLetter = char.ToLower(correctWord[i]);
                char? userLetter = char.ToLower(userWord[i]);
                bool isCorrect = correctLetter == userLetter;

                checkWords.Add(new CheckWord
                {
                    CorrectLetter = correctLetter,
                    UserLetter = userLetter,
                    IsCorrect = (isCorrect == true) ? GameParticipateCorrectEnum.Doğru : GameParticipateCorrectEnum.Yanlış
                });

                if (!isCorrect)
                {
                    wrongCount++;
                }
            }

            if (correctLength > userLength)
            {
                for (int i = count; i < correctLength; i++)
                {
                    char correctLetter = char.ToLower(correctWord[i]);
                    checkWords.Add(new CheckWord
                    {
                        CorrectLetter = correctLetter,
                        UserLetter = '?',
                        IsCorrect = GameParticipateCorrectEnum.Boş
                    });
                    wrongCount++;
                }
            }
            else if (userLength > correctLength)
            {
                for (int i = count; i < userLength; i++)
                {
                    char userLetter = char.ToLower(userWord[i]);
                    checkWords.Add(new CheckWord
                    {
                        CorrectLetter = '?',
                        UserLetter = userLetter,
                        IsCorrect = GameParticipateCorrectEnum.Boş
                    });
                    wrongCount++;
                }
            }

            return new CheckWordResponseDto
            {
                CheckWords = checkWords,
                WrongCount = wrongCount
            };
        }

        public CheckWordResponseDto SearchInCheckWord(string correctWord, string userWord)
        {
            int correctLength = correctWord.Length;
            int userLength = userWord.Length;

            int wrongCount = 0;
            List<CheckWord> checkWords = new List<CheckWord>();

            if (userLength <= correctLength)
            {
                int startIndex = correctWord.IndexOf(userWord, StringComparison.OrdinalIgnoreCase);
                if (startIndex != -1)
                {
                    for (int i = 0; i < startIndex; i++)
                    {
                        checkWords.Add(new CheckWord
                        {
                            CorrectLetter = char.ToLower(correctWord[i]),
                            UserLetter = '?',
                            IsCorrect = GameParticipateCorrectEnum.Boş
                        });
                        wrongCount++;
                    }

                    int endIndex = startIndex + userLength;

                    for (int i = startIndex; i < endIndex; i++)
                    {
                        char correctLetter = correctWord[i];
                        char userLetter = userWord[i - startIndex];

                        bool isCorrect = char.ToLower(correctLetter) == char.ToLower(userLetter);

                        checkWords.Add(new CheckWord
                        {
                            CorrectLetter = correctLetter,
                            UserLetter = userLetter,
                            IsCorrect = (isCorrect == true) ? GameParticipateCorrectEnum.Doğru : GameParticipateCorrectEnum.Yanlış
                        });

                        if (!isCorrect)
                        {
                            wrongCount++;
                        }
                    }

                    for (int i = endIndex; i < correctLength; i++)
                    {
                        checkWords.Add(new CheckWord
                        {
                            CorrectLetter = correctWord[i],
                            UserLetter = '?',
                            IsCorrect = GameParticipateCorrectEnum.Boş
                        });
                        wrongCount++;
                    }
                }
                else
                {
                    return new CheckWordResponseDto();
                }
            }
            else
            {
                return new CheckWordResponseDto();
            }

            return new CheckWordResponseDto
            {
                CheckWords = checkWords,
                WrongCount = wrongCount
            };
        }
    }
}
