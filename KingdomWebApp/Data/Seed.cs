using Microsoft.AspNetCore.Identity;
using WorkshopGroup.Data.Enum;
using WorkshopGroup.Models;

using WorkshopGroup.Data;

namespace WorkshopGroup.Data
{
  public class seed
  {
    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
      using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

        context.Database.EnsureCreated();

        if (!context.Clubs.Any())
        {
          context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "WoodWorking Club",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the WWC",
                            ClubCategory = ClubCategory.WoodWorking,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                         },
                        new Club()
                        {
                            Title = "Metalworking Club",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the MWC",
                            ClubCategory = ClubCategory.Metalworking,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "AL"
                            }
                        },
                        new Club()
                        {
                            Title = "Engraving",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the EG",
                            ClubCategory = ClubCategory.Engraving,
                            Address = new Address()
                            {
                                Street = "133 Main St",
                                City = "Charlotte",
                                State = "AL"
                            }
                        },
                        new Club()
                        {
                            Title = "ThreeDPrinting",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the 3DPC",
                            ClubCategory = ClubCategory.ThreeDprinting,
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
        //Projects
        if (!context.Projects.Any())
        {
          context.Projects.AddRange(new List<Project>()
                    {
                        new Project()
                        {
                            Title = "Building App",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of first proj",
                            ProjectCategory = ProjectCategory.Tooling,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Project()
                        {
                            Title = "proj 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first project",
                            ProjectCategory = ProjectCategory.StationUpgrade,
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

                if (!context.Skills.Any())
                {
                    context.Skills.AddRange(new List<Skill>()
                    {
                        new Skill()
                        {
                            //do not include Id here, it is an 'identity column' or in other words, it is set to auto-increment itself
                            //and not be set explicitely
                            Name = "Programming",
                            Description = "This is the description of the programming skill",    
                        },
                        new Skill()
                        {
                            
                            Name = "Woodworking",
                            Description = "This is the description of the Woodworking skill",
                        },
                        new Skill()
                        {
                            
                            Name = "Troubleshooting",
                            Description = "This is the description of the Troubleshooting skill",
                        }
                    });
                    context.SaveChanges();
                }
            }
    }

    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
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
              Street = "17038 Finnel rd",
              City = "Northport",
              State = "AL"
            }
          };
          await userManager.CreateAsync(newAdminUser, "Coding@1234?");
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