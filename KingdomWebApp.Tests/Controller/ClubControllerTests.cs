using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KingdomWebApp.Controllers;
using KingdomWebApp.Interfaces;
using KingdomWebApp.Models;
using KingdomWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KingdomWebApp.Tests.Controller
{
    public class GuildControllerTests
    {
        private GuildController _guildController;
        private IGuildRepository _guildRepository;
        private IPhotoService _photoService;
        private IHttpContextAccessor _httpContextAccessor;
        public GuildControllerTests()
        {
            //Dependencies
            _guildRepository = A.Fake<IGuildRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();

            //SUT
            _guildController = new GuildController(_guildRepository, _photoService);
        }

        [Fact]
        public void GuildController_Index_ReturnsSuccess()
        {
            //Arrange - What do i need to bring in?
            var guilds = A.Fake<IEnumerable<Guild>>();
            A.CallTo(() => _guildRepository.GetAll()).Returns(guilds);
            //Act
            var result = _guildController.Index();
            //Assert - Object check actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void GuildController_Detail_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var guild = A.Fake<Guild>();
            A.CallTo(() => _guildRepository.GetByIdAsync(id)).Returns(guild);
            //Act
            var result = _guildController.DetailGuild(id, "SkillingGuild");
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }


    }
}
