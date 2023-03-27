using Microsoft.AspNetCore.Identity;
using WorkshopGroup.Data.Enum;
using WorkshopGroup.Models;

using WorkshopGroup.Data;
using ContactWebModels;

namespace WorkshopGroup.Data
{
  public class seedAgain : seed
  {
    private readonly ApplicationDbContext dataContext;
    public seedAgain(ApplicationDbContext context)
    {
      this.dataContext = context;
    }
    public void SeedApplicationDbContext()
    {
      if (!dataContext.SupplyOwners.Any())
      {
        var supplyOwners = new List<SupplyOwner>()
                {
                    new SupplyOwner()
                    {
                        Supply = new Supply()
                        {
                            Name = "Hammer",
                            DateCreated = new DateTime(1903,1,1),
                            SupplyCategories = new List<SupplyCategory>()
                            {
                                new SupplyCategory { Category = new Category() { Name = "Tool"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Pikachu",Comment = "Pickahu is the best supply, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Pikachu", Comment = "Pickachu is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Pikachu",Comment = "Pickchu, pickachu, pikachu", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Jack",
                            LastName = "London",
                            Gym = "Brocks Gym",
                            Country = new Country()
                            {
                                Name = "Kanto"
                            }
                        }
                    },
                    new SupplyOwner()
                    {
                        Supply = new Supply()
                        {
                            Name = "Squirtle",
                            DateCreated = new DateTime(1903,1,1),
                            SupplyCategories = new List<SupplyCategory>()
                            {
                                new SupplyCategory { Category = new Category() { Name = "Water"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Squirtle", Comment = "squirtle is the best supply, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Squirtle",Comment = "Squirtle is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Squirtle", Comment = "squirtle, squirtle, squirtle", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Harry",
                            LastName = "Potter",
                            Gym = "Mistys Gym",
                            Country = new Country()
                            {
                                Name = "Saffron City"
                            }
                        }
                    },
                    new SupplyOwner()
                    {
                        Supply = new Supply()
                        {
                            Name = "Venasuar",
                            DateCreated = new DateTime(1903,1,1),
                            SupplyCategories = new List<SupplyCategory>()
                            {
                                new SupplyCategory { Category = new Category() { Name = "Leaf"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Veasaur",Comment = "Venasuar is the best supply, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Veasaur",Comment = "Venasuar is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Veasaur",Comment = "Venasuar, Venasuar, Venasuar", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Ash",
                            LastName = "Ketchum",
                            Gym = "Ashs Gym",
                            Country = new Country()
                            {
                                Name = "Millet Town"
                            }
                        }
                    }
                };
        dataContext.SupplyOwners.AddRange(supplyOwners);
        dataContext.SaveChanges();
      }
    }
  }
}