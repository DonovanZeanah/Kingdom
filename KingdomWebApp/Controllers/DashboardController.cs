using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KingdomWebApp.Interfaces;
using KingdomWebApp.Models;
using KingdomWebApp.ViewModels;

namespace KingdomWebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRespository;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRespository, IPhotoService photoService)
        {
            _dashboardRespository = dashboardRespository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var userTrades = await _dashboardRespository.GetAllUserTrades();
            var userGuilds = await _dashboardRespository.GetAllUserGuilds();
            var dashboardViewModel = new DashboardViewModel()
            {
                Trades = userTrades,
                Guilds = userGuilds
            };
            return View(dashboardViewModel);
        }
    }
}