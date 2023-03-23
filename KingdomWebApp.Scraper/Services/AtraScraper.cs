using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using KingdomWebApp.Data;
using KingdomWebApp.Data.Enum;
using KingdomWebApp.Interfaces;
using KingdomWebApp.Models;
using KingdomWebApp.Repository;
using KingdomWebApp.Scraper.Data;
using KingdomWebApp.Scraper.Extensions;
using KingdomWebApp.Scraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomWebApp.Scraper.Services
{
    public class AtraScraper : IAtraScraper
    {
        private ITradeRepository _tradeRepo;
        private IWebDriver _driver;
        public AtraScraper()
        {
            _driver = new ChromeDriver();
        }

        public void Run()
        {
            IterateOverTradeElements();
        }

        public IReadOnlyCollection<IWebElement> GetElements()
        {
            return _driver.FindElements(By.CssSelector("span[itemprop='name']"));
        }

        public void IterateOverTradeElements()
        {
            _driver.Navigate().GoToUrl("https://trailQuestner.com/trade-calendar/");
            var pageNumbers = _driver.FindElements(By.CssSelector("a[class='page-numbers']"));
            for (int i = 0; i < pageNumbers.Count; i++)
            {
                    if(i >= 1)
                    {
                        pageNumbers = _driver.FindElements(By.CssSelector("a[class='page-numbers']"));
                        pageNumbers.ElementAt(i).Click();
                    }
                    var pageElements = GetElements();
                    for (int j = 0; j < pageElements.Count; j++)
                    {
                        try
                        {
                            pageElements = _driver.FindElements(By.CssSelector("span[itemprop='name']"));
                            var element = pageElements.ElementAt(j);
                            element.Click();
                            var trade = new Trade()
                            {
                                Title = _driver.FindElement(By.CssSelector("h1[class='event-title']")).Text ?? "",
                                Description = _driver.FindElement(By.CssSelector("div[class='content-desc']")).Text.Replace("Description:", "") ?? "",
                                StartTime = _driver.FindElement(By.CssSelector("meta[itemprop='startDate']")).GetAttribute("content").ToDate() ?? DateTime.MinValue,
                                EntryFee = int.Parse(_driver.FindElement(By.CssSelector("span[itemprop='price']")).Text),
                                Website = _driver.FindElement(By.LinkText("Visit Event Website")).GetAttribute("href") ?? "",
                                Twitter = _driver.FindElement(By.LinkText("Event Twitter feed")).GetAttribute("href") ?? "",
                                Facebook = _driver.FindElement(By.LinkText("Event Facebook page")).GetAttribute("href") ?? "",
                                Contact = _driver.FindElement(By.LinkText("Contact Trade Director")).GetAttribute("href") ?? "",
                                Address = new Address()
                                {
                                    City = _driver.FindElement(By.CssSelector("span[itemprop='addressLocality']")).Text ?? "",
                                    State = _driver.FindElement(By.CssSelector("span[itemprop='addressRegion']")).Text ?? "",
                                    Street = _driver.FindElement(By.CssSelector("div[itemprop='address']")).Text ?? "",
                                },
                                TradeCategory = TradeCategory.Ultra
                            };
                            using (var context = new ScraperDBContext())
                            {
                                if (!context.Trades.Any(r => r.Title == trade.Title))
                                {
                                    context.Trades.Add(trade);
                                    context.SaveChanges();
                                }
                            }
                            _driver.Navigate().Back();
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine(ex.Message);
                            _driver.Navigate().Back();
                        }
                }
            }
        }
    }
}
