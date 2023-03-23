using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using KingdomWebApp.Data;
using KingdomWebApp.Data.Enum;
using KingdomWebApp.Models;
using KingdomWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KingdomWebApp.Tests.Repository
{
    public class GuildRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if(await databaseContext.Guilds.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Guilds.Add(
                      new Guild()
                      {
                          Title = "Skilling Guild 1",
                          Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                          Description = "This is the description of the first cinema",
                          GuildCategory = GuildCategory.City,
                          Address = new Address()
                          {
                              Street = "123 Main St",
                              City = "Charlotte",
                              State = "NC"
                          }
                      });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void GuildRepository_Add_ReturnsBool()
        {
            //Arrange
            var guild = new Guild()
            {
                Title = "Skilling Guild 1",
                Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                Description = "This is the description of the first cinema",
                GuildCategory = GuildCategory.City,
                Address = new Address()
                {
                    Street = "123 Main St",
                    City = "Charlotte",
                    State = "NC"
                }
            };
            var dbContext = await GetDbContext();
            var guildRepository = new GuildRepository(dbContext);

            //Act
            var result = guildRepository.Add(guild);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void GuildRepository_GetByIdAsync_ReturnsGuild()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var guildRepository = new GuildRepository(dbContext);

            //Act
            var result = guildRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Guild>>();
        }

        [Fact]
        public async void GuildRepository_GetAll_ReturnsList()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var guildRepository = new GuildRepository(dbContext);

            //Act
            var result = await guildRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Guild>>();
        }

        [Fact]
        public async void GuildRepository_SuccessfulDelete_ReturnsTrue()
        {
            //Arrange
            var guild = new Guild()
            {
                Title = "Skilling Guild 1",
                Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                Description = "This is the description of the first cinema",
                GuildCategory = GuildCategory.City,
                Address = new Address()
                {
                    Street = "123 Main St",
                    City = "Charlotte",
                    State = "NC"
                }
            };
            var dbContext = await GetDbContext();
            var guildRepository = new GuildRepository(dbContext);

            //Act
            guildRepository.Add(guild);
            var result = guildRepository.Delete(guild);
            var count = await guildRepository.GetCountAsync();

            //Assert
            result.Should().BeTrue();
            count.Should().Be(0);
        }

        [Fact]
        public async void GuildRepository_GetCountAsync_ReturnsInt()
        {
            //Arrange
            var guild = new Guild()
            {
                Title = "Skilling Guild 1",
                Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                Description = "This is the description of the first cinema",
                GuildCategory = GuildCategory.City,
                Address = new Address()
                {
                    Street = "123 Main St",
                    City = "Charlotte",
                    State = "NC"
                }
            };
            var dbContext = await GetDbContext();
            var guildRepository = new GuildRepository(dbContext);

            //Act
            guildRepository.Add(guild);
            var result = await guildRepository.GetCountAsync();

            //Assert
            result.Should().Be(1);
        }

        [Fact]
        public async void GuildRepository_GetAllStates_ReturnsList()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var guildRepository = new GuildRepository(dbContext);

            //Act
            var result = await guildRepository.GetAllStates();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<State>>();
        }

        [Fact]
        public async void GuildRepository_GetGuildsByState_ReturnsList()
        {
            //Arrange
            var state = "NC";
            var guild = new Guild()
            {
                Title = "Skilling Guild 1",
                Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                Description = "This is the description of the first cinema",
                GuildCategory = GuildCategory.City,
                Address = new Address()
                {
                    Street = "123 Main St",
                    City = "Charlotte",
                    State = "NC"
                }
            };
            var dbContext = await GetDbContext();
            var guildRepository = new GuildRepository(dbContext);

            //Act
            guildRepository.Add(guild);
            var result = await guildRepository.GetGuildsByState(state);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Guild>>();
            result.First().Title.Should().Be("Skilling Guild 1");
        }
    }
}
