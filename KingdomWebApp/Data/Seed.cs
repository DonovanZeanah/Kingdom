using Microsoft.AspNetCore.Identity;
using KingdomWebApp.Data.Enum;
using KingdomWebApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KingdomWebApp.Data
{
    public class Seed
    {
        private readonly IConfiguration _configuration;

        public Seed(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SeedData(IApplicationBuilder applicationBuilder)
        {
            

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Guilds.Any())
                {
                    context.Guilds.AddRange(new List<Guild>()
                    {
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
                         },
                        new Guild()
                        {
                            Title = "Skilling Guild 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            GuildCategory = GuildCategory.Endurance,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Guild()
                        {
                            Title = "Skilling Guild 3",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first guild",
                            GuildCategory = GuildCategory.Trail,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Guild()
                        {
                            Title = "Skilling Guild 3",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first guild",
                            GuildCategory = GuildCategory.City,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Michigan",
                                State = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Trades
                if (!context.Trades.Any())
                {
                    context.Trades.AddRange(new List<Trade>()
                    {
                        new Trade()
                        {
                            Title = "Skilling Trade 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first trade",
                            TradeCategory = TradeCategory.Marathon,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Trade()
                        {
                            Title = "Skilling Trade 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first trade",
                            TradeCategory = TradeCategory.Ultra,
                            AddressId = 5,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "dkzeanah@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "DKZeanah",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Streety St",
                            City = "Tuscaloosa",
                            State = "Al"
                        }
                    };

                     
                    await userManager.CreateAsync(newAdminUser, _configuration["UserSecrets:AdminPassword"]);
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}