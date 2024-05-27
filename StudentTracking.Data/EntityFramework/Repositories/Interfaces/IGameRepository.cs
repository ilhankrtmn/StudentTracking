using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;

namespace StudentTracking.Data.EntityFramework.Repositories.Interfaces
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        Task<List<Game>> GetAllGamesAsync();
        Task<GameConfiguration> GetGameConfiguration(int gameID);
        Task<GameConfiguration> GetGameConfiguration(int gameID, int configurationID);
        Task<int> AddGameTransaction(GameTransactionSaveDto gameTransactionSaveDto);
    }
}
