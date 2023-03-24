using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KingdomWebApp.Data;
using KingdomWebApp.Interfaces;
using KingdomWebApp.Models;
using KingdomWebApp.ViewModels;
using KingdomWebApp.Models.Enum;

namespace KingdomWebApp.Controllers
{
    public class TradeController : Controller
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TradeController(ITradeRepository tradeRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _tradeRepository = tradeRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int category = -1, int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }

            // if category is -1 (All) dont filter else filter by selected category
            var trades = category switch
            {
                -1 => await _tradeRepository.GetSliceAsync((page - 1) * pageSize, pageSize),
                _ => await _tradeRepository.GetTradesByCategoryAndSliceAsync((TradeCategory)category, (page - 1) * pageSize, pageSize),
            };

            var count = category switch
            {
                -1 => await _tradeRepository.GetCountAsync(),
                _ => await _tradeRepository.GetCountByCategoryAsync((TradeCategory)category),
            };

            var viewModel = new IndexTradeViewModel
            {
                Trades = trades,
                Page = page,
                PageSize = pageSize,
                TotalTrades = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Category = category,
            };

            return View(viewModel);
        }

        [HttpGet]
        [Route("event/{QuestTrade}/{id}")]
        public async Task<IActionResult> DetailTrade(int id, string QuestTrade)
        {
            var trade = await _tradeRepository.GetByIdAsync(id);
            return trade == null ? NotFound() : View(trade);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var curUserID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createTradeViewModel = new CreateTradeViewModel { AppUserId = curUserID };
            return View(createTradeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTradeViewModel tradeVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(tradeVM.Image);

                var trade = new Trade
                {
                    Title = tradeVM.Title,
                    Description = tradeVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = tradeVM.AppUserId,
                    TradeCategory = tradeVM.TradeCategory,
                    Address = new Address
                    {
                        Street = tradeVM.Address.Street,
                        City = tradeVM.Address.City,
                        State = tradeVM.Address.State,
                    }
                };
                _tradeRepository.Add(trade);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(tradeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var trade = await _tradeRepository.GetByIdAsync(id);
            if (trade == null) return View("Error");
            var tradeVM = new EditTradeViewModel
            {
                Title = trade.Title,
                Description = trade.Description,
                AddressId = trade.AddressId,
                Address = trade.Address,
                URL = trade.Image,
                TradeCategory = trade.TradeCategory
            };
            return View(tradeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTradeViewModel tradeVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit guild");
                return View(tradeVM);
            }

            var userTrade = await _tradeRepository.GetByIdAsyncNoTracking(id);

            if (userTrade == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(tradeVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(tradeVM);
            }

            if (!string.IsNullOrEmpty(userTrade.Image))
            {
                _ = _photoService.DeletePhotoAsync(userTrade.Image);
            }

            var trade = new Trade
            {
                Id = id,
                Title = tradeVM.Title,
                Description = tradeVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = tradeVM.AddressId,
                Address = tradeVM.Address,
            };

            _tradeRepository.Update(trade);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var guildDetails = await _tradeRepository.GetByIdAsync(id);
            if (guildDetails == null) return View("Error");
            return View(guildDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteGuild(int id)
        {
            var tradeDetails = await _tradeRepository.GetByIdAsync(id);

            if (tradeDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(tradeDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(tradeDetails.Image);
            }

            _tradeRepository.Delete(tradeDetails);
            return RedirectToAction("Index");
        }

        public IActionResult Detail()
        {
            throw new NotImplementedException();
        }
    }
}