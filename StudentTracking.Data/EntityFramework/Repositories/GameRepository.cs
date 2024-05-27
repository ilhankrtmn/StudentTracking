using StudentTracking.Data.EntityFramework.Base;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.Enums;
using StudentTracking.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentTracking.Data.EntityFramework.Repositories
{
    public class GameRepository : EfCoreRepositoryBase<Game>, IGameRepository
    {
        private readonly StudentTrackingContext _context;

        public GameRepository(StudentTrackingContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Game>> GetAllGamesAsync()
        {
            return _context.Games.Where(p => p.Status == 1).ToListAsync();
        }

        public async Task<GameConfiguration> GetGameConfiguration(int gameID)
        {
            Random random = new Random();
            var gameConfigurations = await _context.GameConfigurations.Where(p => p.GameID == gameID).ToListAsync();
            return gameConfigurations.OrderBy(x => random.Next()).Take(1).FirstOrDefault();

            /* Eğerki çekilen datanın fazla olmasını istiyorsan alttaki şekilde 3 tane rastgele oyun bilgisi alabilrisin.
            var gameConfigurations = await _context.GameConfigurations.Where(p => p.GameID == gameID).ToListAsync();
            gameConfigurations.OrderBy(x => random.Next()).Take(3).ToList();
            */
        }

        public async Task<GameConfiguration> GetGameConfiguration(int gameID, int configurationID)
        {
            Random random = new Random();
            var gameConfigurations = await _context.GameConfigurations.Where(p => p.GameID == gameID && p.ID == configurationID).ToListAsync();
            return gameConfigurations.OrderBy(x => random.Next()).Take(1).FirstOrDefault();
        }


        public async Task<int> AddGameTransaction(GameTransactionSaveDto gameTransactionSaveDto)
        {
            var gameTransaction = new GameTransaction
            {
                CustomerID = gameTransactionSaveDto.CustomerID,
                GameID = gameTransactionSaveDto.GameID,
                GameConfigurationID = gameTransactionSaveDto.GameConfigurationID,
                InputWord = gameTransactionSaveDto.InputWord,
                Status = GameTransactionEnum.Active
            };

            _context.GameTransactions.Add(gameTransaction);
            await _context.SaveChangesAsync();

            return gameTransaction.EntryID;
        }
    }
}
