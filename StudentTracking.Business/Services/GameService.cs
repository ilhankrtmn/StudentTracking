using StudentTracking.Business.Interfaces;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using Newtonsoft.Json;
using StudentTracking.Data.Models;

namespace StudentTracking.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICustomerRepository _customerRepository;
        //private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository, ICustomerRepository customerRepository/*,ILogger<GameService> logger*/)
        {
            _gameRepository = gameRepository;
            _customerRepository = customerRepository;
            //_logger = logger;
        }

        public async Task<List<Game>> GetGameAsync()
        {
            return await _gameRepository.GetAllGamesAsync();
        }

        public async Task<GameConfiguration> GetGameConfiguration(int gameID)
        {
            return await _gameRepository.GetGameConfiguration(gameID);
        }
        public async Task<GameConfiguration> GetGameConfiguration(int gameID, int configurationID)
        {
            return await _gameRepository.GetGameConfiguration(gameID, configurationID);
        }

        public async Task<Game> GetGame(int gameID)
        {
            return await _gameRepository.FindAsync(p => p.GameID == gameID);
        }

        public async Task<int> AddGameTransaction(GameTransactionSaveDto gameTransactionSaveDto)
        {
            return await _gameRepository.AddGameTransaction(gameTransactionSaveDto);
        }

        public async Task AddCustomerGameTransaction(CustomerSaveTransactionDto customerSaveTransactionDto)
        {
            await _customerRepository.AddCustomerTransaction(customerSaveTransactionDto);
        }

        public async Task<List<GameHistoryResponseDto>> GetGameHistory(int customerID)
        {
            var customerTransactions = await _customerRepository.GetGameHistory(customerID);
            var transactionDataList = new List<GameHistoryResponseDto>();

            foreach (var item in customerTransactions)
            {
                var transactionDetail = JsonConvert.DeserializeObject<CheckWordResponseDto>(item.TransactionDetail);
                var transactionData = new GameHistoryResponseDto
                {
                    CheckWordResponseDtos = transactionDetail,
                    CreateDate = item.Createdate
                };

                transactionDataList.Add(transactionData);
            }

            return transactionDataList;
        }
    }
}
