using Microsoft.AspNetCore.Identity;
using KingdomWebApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using KingdomWebApp.Models.Enum;
using KingdomWebApp.Data;

namespace KingdomWebApp.Helpers
{
    public class Seed
    {
        private readonly IConfiguration _configuration;

        public Seed(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected Seed()
        {
            throw new NotImplementedException();
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
                            Title = "Programming",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            GuildCategory = GuildCategory.Craft,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Tuscaloosa",
                                State = "AL"
                            }
                         },
                        new Guild()
                        {
                            Title = "WoodWorking",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            GuildCategory = GuildCategory.Endurance,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Tuscaloosa",
                                State = "AL"
                            }
                        },
                        new Guild()
                        {
                            Title = "College",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first guild",
                            GuildCategory = GuildCategory.Trail,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Tuscaloosa",
                                State = "AL"
                            }
                        },
                        new Guild()
                        {
                            Title = "Mercedes",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first guild",
                            GuildCategory = GuildCategory.Craft,
                            GuildSubcategory = GuildSubcategory.Automotive,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Tuscaloosa",
                                State = "AL"
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
                            Title = "Programmer",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first trade",
                            TradeCategory = TradeCategory.Digital ,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Trade()
                        {
                            Title = "WoodCrafter",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/Quest.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first trade",
                            TradeCategory = TradeCategory.Craftsman,
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

        // This method seeds users and roles into the Identity database.

        public async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            // Create a new scope for dependency injection.

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                // Retrieve RoleManager to manage Identity roles.

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Create the Admin role if it doesn't exist.

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                // Create the User role if it doesn't exist.

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                // Retrieve UserManager to manage Identity users.

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                string adminUserEmail = "dkzeanah@gmail.com";
                // Find the admin user by email.

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);

                // If the admin user doesn't exist, create a new one.

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
                    // Create the new admin user with a predefined password from configuration.

                    await userManager.CreateAsync(newAdminUser, _configuration["UserSecrets:AdminPassword"]);

                    // Assign the new admin user to the Admin role.

                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                // Find the regular user by email.

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);

                // If the regular user doesn't exist, create a new one.

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
                    // Create the new regular user with a predefined password.

                    await userManager.CreateAsync(newAppUser, "Program!1234?");

                    // Assign the new regular user to the User role.

                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}