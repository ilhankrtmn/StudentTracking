using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;

namespace StudentTracking.Business.Interfaces
{
    public interface IGameService
    {
        Task<List<Game>> GetGameAsync();
        Task<GameConfiguration> GetGameConfiguration(int gameID);
        Task<GameConfiguration> GetGameConfiguration(int gameID, int configurationID);
        Task<Game> GetGame(int gameID);
        Task<int> AddGameTransaction(GameTransactionSaveDto gameTransactionSaveDto);
        Task AddCustomerGameTransaction(CustomerSaveTransactionDto customerSaveTransactionDto);
        Task<List<GameHistoryResponseDto>> GetGameHistory(int customerID);
    }
}
