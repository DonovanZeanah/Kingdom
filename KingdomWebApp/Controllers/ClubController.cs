using Microsoft.AspNetCore.Mvc;
using KingdomWebApp.Helpers;
using KingdomWebApp.Interfaces;
using KingdomWebApp.Models;
using KingdomWebApp.ViewModels;
using KingdomWebApp.Models.Enum;

namespace KingdomWebApp.Controllers
{
    public class GuildController : Controller
    {
        private readonly IGuildRepository _guildRepository;
        private readonly IPhotoService _photoService;

        public GuildController(IGuildRepository guildRepository, IPhotoService photoService)
        {
            _guildRepository = guildRepository;
            _photoService = photoService;
        }

        [HttpGet]
        [Route("SkillingGuilds")]
        public async Task<IActionResult> Index(int category = -1, int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }

            // if category is -1 (All) dont filter else filter by selected category
            var guilds = category switch
            {
                -1 => await _guildRepository.GetSliceAsync((page - 1) * pageSize, pageSize),
                _ => await _guildRepository.GetGuildsByCategoryAndSliceAsync((GuildCategory)category, (page - 1) * pageSize, pageSize),
            };

            var count = category switch
            {
                -1 => await _guildRepository.GetCountAsync(),
                _ => await _guildRepository.GetCountByCategoryAsync((GuildCategory)category),
            };

            var guildViewModel = new IndexGuildViewModel
            {
                Guilds = guilds,
                Page = page,
                PageSize = pageSize,
                TotalGuilds = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Category = category,
            };

            return View(guildViewModel);
        }

        [HttpGet]
        [Route("SkillingGuilds/{state}")]
        public async Task<IActionResult> ListGuildsByState(string state)
        {
            var guilds = await _guildRepository.GetGuildsByState(StateConverter.GetStateByName(state).ToString());
            var guildVM = new ListGuildByStateViewModel()
            {
                Guilds = guilds
            };
            if (guilds.Count() == 0)
            {
                guildVM.NoGuildWarning = true;
            }
            else
            {
                guildVM.State = state;
            }
            return View(guildVM);
        }

        [HttpGet]
        [Route("SkillingGuilds/{city}/{state}")]
        public async Task<IActionResult> ListGuildsByCity(string city, string state)
        {
            var guilds = await _guildRepository.GetGuildByCity(city);
            var guildVM = new ListGuildByCityViewModel()
            {
                Guilds = guilds
            };
            if (guilds.Count() == 0)
            {
                guildVM.NoGuildWarning = true;
            }
            else
            {
                guildVM.State = state;
                guildVM.City = city;
            }
            return View(guildVM);
        }

        [HttpGet]
        [Route("guild/{QuestGuild}/{id}")]
        public async Task<IActionResult> DetailGuild(int id, string QuestGuild)
        {
            var guild = await _guildRepository.GetByIdAsync(id);

            return guild == null ? NotFound() : View(guild);
        }

        [HttpGet]
        [Route("SkillingGuilds/State")]
        public async Task<IActionResult> SkillingGuildsByStateDirectory()
        {
            var states = await _guildRepository.GetAllStates();
            var guildVM = new SkillingGuildByState()
            {
                States = states
            };

            return states == null ? NotFound() : View(guildVM);
        }

        [HttpGet]
        [Route("SkillingGuilds/State/City")]
        public async Task<IActionResult> SkillingGuildsByStateForCityDirectory()
        {
            var states = await _guildRepository.GetAllStates();
            var guildVM = new SkillingGuildByState()
            {
                States = states
            };

            return states == null ? NotFound() : View(guildVM);
        }

        [HttpGet]
        [Route("SkillingGuilds/{state}/City")]
        public async Task<IActionResult> SkillingGuildsByCityDirectory(string state)
        {
            var cities = await _guildRepository.GetAllCitiesByState(StateConverter.GetStateByName(state).ToString());
            var guildVM = new SkillingGuildByCity()
            {
                Cities = cities
            };

            return cities == null ? NotFound() : View(guildVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var curUserId = HttpContext.User.GetUserId();
            var createGuildViewModel = new CreateGuildViewModel { AppUserId = curUserId };
            return View(createGuildViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGuildViewModel guildVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(guildVM.Image);

                var guild = new Guild
                {
                    Title = guildVM.Title,
                    Description = guildVM.Description,
                    Image = result.Url.ToString(),
                    GuildCategory = guildVM.GuildCategory,
                    AppUserId = guildVM.AppUserId,
                    Address = new Address
                    {
                        Street = guildVM.Address.Street,
                        City = guildVM.Address.City,
                        State = guildVM.Address.State,
                    }
                };
                _guildRepository.Add(guild);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(guildVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var guild = await _guildRepository.GetByIdAsync(id);
            if (guild == null) return View("Error");
            var guildVM = new EditGuildViewModel
            {
                Title = guild.Title,
                Description = guild.Description,
                AddressId = guild.AddressId,
                Address = guild.Address,
                URL = guild.Image,
                GuildCategory = guild.GuildCategory
            };
            return View(guildVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditGuildViewModel guildVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit guild");
                return View("Edit", guildVM);
            }

            var userGuild = await _guildRepository.GetByIdAsyncNoTracking(id);

            if (userGuild == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(guildVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(guildVM);
            }

            if (!string.IsNullOrEmpty(userGuild.Image))
            {
                _ = _photoService.DeletePhotoAsync(userGuild.Image);
            }

            var guild = new Guild
            {
                Id = id,
                Title = guildVM.Title,
                Description = guildVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = guildVM.AddressId,
                Address = guildVM.Address,
            };

            _guildRepository.Update(guild);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var guildDetails = await _guildRepository.GetByIdAsync(id);
            if (guildDetails == null) return View("Error");
            return View(guildDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteGuild(int id)
        {
            var guildDetails = await _guildRepository.GetByIdAsync(id);

            if (guildDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(guildDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(guildDetails.Image);
            }

            _guildRepository.Delete(guildDetails);
            return RedirectToAction("Index");
        }
    }
}