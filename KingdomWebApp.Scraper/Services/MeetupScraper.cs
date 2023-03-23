using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using KingdomWebApp.Data.Enum;
using KingdomWebApp.Extensions;
using KingdomWebApp.Models;
using KingdomWebApp.Scraper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomWebApp.Scraper.Services
{
    public class MeetupScraper
    {
        private IWebDriver _driver;
        public MeetupScraper()
        {
            _driver = new ChromeDriver();
        }

        public void Run()
        {
            GetListOfCityAndState();
        }

        public void GetListOfCityAndState()
        {
            int batchSize = 100;
            int currentBatch = 0;
            bool done = false;
            while(!done)
            using (var context = new ScraperDBContext())
            {
               var cities = context.Cities.OrderBy(x => x.Id).Skip(currentBatch++ * batchSize).Take(batchSize).ToList();
                foreach(var city in cities)
                {
                    IterateOverSkillingGuilds(city.StateCode.ToLower(), city.CityName.ToLower());
                    if(city.Id == 40000)
                    {
                       done = true;
                    }
                }
            }
        }


        public void IterateOverSkillingGuilds(string state, string city)
        {
            try
            {
                _driver.Navigate().GoToUrl($"https://www.meetup.com/find/?suggested=true&source=GROUPS&keywords=Quest%20guild&location=us--{state}--{city}");
                //System.Threading.Thread.Sleep(1000);
                var pageElements = _driver.FindElements(By.CssSelector("h3[data-testid='group-card-title']"));
                for (int i = 0; i < pageElements.Count; i++)
                {
                    try
                    {
                        pageElements = _driver.FindElements(By.CssSelector("h3[data-testid='group-card-title']"));
                        var element = pageElements.ElementAt(i);
                        var placeholder = element.Text;
                        var placeholder2 = element.Text.Contains("Quest", System.StringComparison.CurrentCultureIgnoreCase);
                        if (element.Text.Contains("Quest", System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            element.Click();
                            var guild = new Guild()
                            {
                                Title = _driver.FindElement(By.CssSelector("a[class='groupHomeHeader-groupNameLink']")).Text ?? "",
                                Description = _driver.FindElement(By.CssSelector("p[class='group-description _groupDescription-module_description__3qvYh margin--bottom']")).Text ?? "",
                                Address = new Address()
                                {
                                    State = state.ToUpper(),
                                    City = city.FirstCharToUpper()
                                },
                                GuildCategory = GuildCategory.City
                            };
                            using (var context = new ScraperDBContext())
                            {
                                if (!context.Guilds.Any(c => c.Title == guild.Title))
                                {
                                    context.Guilds.Add(guild);
                                    context.SaveChanges();
                                }
                            }
                            _driver.Navigate().Back();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                        _driver.Navigate().Back();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

    }
}
