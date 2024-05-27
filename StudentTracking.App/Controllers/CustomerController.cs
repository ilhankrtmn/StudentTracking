using FluentValidation.Results;
using StudentTracking.App.Models;
using StudentTracking.Business.Interfaces;
using StudentTracking.Business.ValidationRules;
using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StudentTracking.App.Controllers
{
    public class CustomerController : Controller
    {
        //private readonly ILogger _logger;
        private readonly IGameService _gameService;
        private readonly IRegisterService _registerService;
        private readonly IEmailService _emailService;
        private readonly ICheckWordService _checkWordService;

        public CustomerController(IGameService gameService, IRegisterService registerService, IEmailService emailService, ICheckWordService checkWordService)
        {
            _gameService = gameService;
            _registerService = registerService;
            _emailService = emailService;
            _checkWordService = checkWordService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ErrorPage(int code)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            CustomerRegisterBody customerRegisterBody = new CustomerRegisterBody();

            customerRegisterBody.CityList = await _registerService.GetCitiesAsync();

            return View(customerRegisterBody);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegisterBody customerRegisterBody)
        {
            Customer customer = new Customer();
            customer.Name = customerRegisterBody.Name;
            customer.Surname = customerRegisterBody.Surname;
            customer.Email = customerRegisterBody.Email;
            customer.CityID = customerRegisterBody.CityID;
            customer.Password = customerRegisterBody.Password;

            customerRegisterBody.CityList = await _registerService.GetCitiesAsync();

            RegisterValidator registerValidator = new RegisterValidator();
            ValidationResult validationResult = registerValidator.Validate(customer);

            if (validationResult.IsValid)
            {
                if (await _registerService.CheckRegisterMail(customerRegisterBody.Email))
                {
                    await _registerService.InsertCustomer(customer);
                    return RedirectToAction("GameList", "Customer");
                }
                else
                {
                    //TODO else bloğuna düşerse yönlendirme alanını değiştir.
                    return RedirectToAction("Login", "Customer");
                }
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(customerRegisterBody);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CustomerLoginBody customerLoginBody)
        {
            var customerID = await _registerService.CheckCustomerLogin(customerLoginBody.Email, customerLoginBody.Password);
            if (customerID != 0)
            {
                HttpContext.Session.SetString("customerID", customerID.ToString());
                return RedirectToAction("GameList", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        //[Authorize]
        public async Task<IActionResult> GameList()
        {
            var customerID = HttpContext.Session.GetString("customerID");
            GameforListPage theGameforListPage = new GameforListPage();

            try
            {
                if (Convert.ToInt32(customerID) > 0)
                {
                    theGameforListPage.Games = await _gameService.GetGameAsync();
                    HttpContext.Session.SetString("customerID", customerID);
                }
                else
                {
                    return RedirectToAction("Login", "Customer");
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message, ex);
                throw;
            }

            return View(theGameforListPage);
        }

        [HttpGet]
        public async Task<IActionResult> ParticipateGame(int gameID)
        {
            ParticipateGamePage particapateGamePage = new ParticipateGamePage();
            var customerID = HttpContext.Session.GetString("customerID");

            try
            {
                if (Convert.ToInt32(customerID) > 0)
                {
                    particapateGamePage.GameConfiguration = await _gameService.GetGameConfiguration(gameID);
                    particapateGamePage.Game = await _gameService.GetGame(gameID);
                    HttpContext.Session.SetString("customerID", customerID);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(particapateGamePage);
        }

        [HttpPost]
        public async Task<IActionResult> ParticipateGame(ParticipateGameBody participateGameBody)
        {
            ParticipateGamePage particapateGamePage = new ParticipateGamePage();
            int customerID = Convert.ToInt32(HttpContext.Session.GetString("customerID"));

            particapateGamePage.GameConfiguration = await _gameService.GetGameConfiguration(participateGameBody.GameID, participateGameBody.ID);
            particapateGamePage.Game = await _gameService.GetGame(participateGameBody.GameID);

            var checkWordResponse = _checkWordService.CheckWord(particapateGamePage.GameConfiguration.Word, participateGameBody.InputWord);

            int gameTransactionID = await _gameService.AddGameTransaction(new GameTransactionSaveDto
            {
                CustomerID = customerID,
                GameID = participateGameBody.GameID,
                GameConfigurationID = participateGameBody.ID,
                InputWord = participateGameBody.InputWord
            });

            await _gameService.AddCustomerGameTransaction(new CustomerSaveTransactionDto
            {
                CustomerID = customerID,
                GameTransactionID = gameTransactionID,
                GameID = participateGameBody.GameID,
                TransactionDetail = JsonConvert.SerializeObject(checkWordResponse)
            });

            return Json(checkWordResponse);
        }

        [HttpGet]
        public async Task<IActionResult> AccountInformation()
        {
            Customer customer;
            int customerID = Convert.ToInt32(HttpContext.Session.GetString("customerID"));

            try
            {
                if (customerID > 0)
                {
                    customer = await _registerService.GetCustomer(customerID);
                    HttpContext.Session.SetString("customerID", customerID.ToString());
                }
                else
                {
                    return RedirectToAction("Login", "Customer");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<JsonResult> AccountInformation(Customer customer)
        {
            Customer theCustomer;

            int customerID = Convert.ToInt32(HttpContext.Session.GetString("customerID"));
            try
            {
                if (customerID > 0)
                {
                    theCustomer = await _registerService.GetCustomer(customerID);
                    theCustomer.Name = customer.Name;
                    theCustomer.Surname = customer.Surname;
                    theCustomer.Password = customer.Password;
                    theCustomer.CustomerID = customerID;

                    await _registerService.UpdateCustomer(theCustomer);
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GameHistory()
        {
            int customerID = Convert.ToInt32(HttpContext.Session.GetString("customerID"));
            GameHistoryListPage gameHistoryListPage = new GameHistoryListPage();
            try
            {
                gameHistoryListPage.GameHistoryResponseDtos = await _gameService.GetGameHistory(customerID);
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(gameHistoryListPage);
        }
    }
}
