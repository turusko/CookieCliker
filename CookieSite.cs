using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CookieClicks
{
    public class CookieSite
    {
        private readonly WebDriver _webdriver;
        private readonly IWebElement _cookie;

        public CookieSite()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webdriver = new ChromeDriver(chromeOptions);
            _webdriver.Navigate().GoToUrl("https://orteil.dashnet.org/cookieclicker/");
            var consent = _webdriver.FindElement(By.CssSelector("button[aria-label='Consent']"));
            new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(consent)).Click();
            _webdriver.FindElement(By.Id("langSelect-EN")).Click();
            _cookie = _webdriver.FindElement(By.Id("bigCookie"));
            _webdriver.Manage().Window.Maximize();
        }

        public void ClickCookie()
        {
            _cookie.Click();
        }

        public int GetCookieCount()
        {
            var CookeText = _webdriver.FindElement(By.Id("cookies")).Text;
            return int.Parse(CookeText.Split(' ')[0]);
        }

        public bool IsUpgradeAvaliable()
        {
            var upgrades = _webdriver.FindElements(By.CssSelector(".crate.upgrade.enabled"));
            return upgrades.Count > 0;
        }

        public void GetUpgrades()
        {
            while (_webdriver.FindElements(By.CssSelector(".crate.upgrade.enabled")).Count > 0)
            {
                _webdriver.FindElement(By.CssSelector(".crate.upgrade.enabled")).Click();
                Thread.Sleep(100);
            }
        }

        public bool IsProductAvaliable()
        {
            var products = _webdriver.FindElements(By.CssSelector(".product.unlocked.enabled"));
            return products.Count > 0;
        }

        public void GetProducts()
        {
            var products = _webdriver.FindElements(By.CssSelector(".product.unlocked.enabled"));

            products.Reverse();

            foreach (var product in products)
            {
                while (product.GetAttribute("class").Contains("enabled"))
                {
                    product.Click();
                }
            }
        }

        public void Close()
        {
            _webdriver.Close();
        }

        public void SaveToFile()
        {
            _webdriver.FindElement(By.CssSelector("#prefsButton")).Click();
            _webdriver.FindElements(By.CssSelector(".smallFancyButton")).First(button => button.Text == "Save to file").Click();
            _webdriver.FindElement(By.CssSelector("#prefsButton")).Click();
        }

        public void LoadFromFile(string uploadFile)
        {
            _webdriver.FindElement(By.CssSelector("#prefsButton")).Click();
            IWebElement fileInput = _webdriver.FindElement(By.CssSelector("input[type=file]"));
            fileInput.SendKeys(uploadFile);
            _webdriver.FindElement(By.CssSelector("#prefsButton")).Click();
        }
    }
}