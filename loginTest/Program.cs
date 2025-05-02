using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

class SeleniumTest
{
    static void Main(string[] args)
    {
        // Set up Chrome options
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");

        // Initialize the Chrome driver
        using (IWebDriver driver = new ChromeDriver(options))
        {
            string[] usernames = { "adnan@gmail.com", "student", "adnan@gmail.com" };
            string[] passwords = { "000000", "sdfgsiu", "99999" };
            int[] expected = { 1, 0, 1 };
            int[] actual = { 0, 0, 0 };

            for (int i = 0; i < usernames.Length; i++)
            {
                Console.WriteLine("Test no: " + i);

                driver.Navigate().GoToUrl("https://nogorprobaho.netlify.app");
                Thread.Sleep(3000); // Wait for page to load

                // Enter username
                driver.FindElement(By.CssSelector("#root > div > div > div.flex.flex-col.flex-1 > div > div > div:nth-child(2) > form > div > div:nth-child(1) > div > input"))
                      .SendKeys(usernames[i]);

                // Enter password
                driver.FindElement(By.CssSelector("#root > div > div > div.flex.flex-col.flex-1 > div > div > div:nth-child(2) > form > div > div:nth-child(2) > div > div > input"))
                      .SendKeys(passwords[i]);

                // Click Login button
                driver.FindElement(By.CssSelector("#root > div > div > div.flex.flex-col.flex-1 > div > div > div:nth-child(2) > form > div > div:nth-child(4) > button"))
                      .Click();

                Thread.Sleep(5000); // Wait for login to complete

                // Check if login was successful
                if (driver.Title == "Logged In Successfully | Practice Test Automation")
                {
                    actual[i] = 1;
                }

                if (expected[i] == actual[i])
                {
                    Console.WriteLine($"Test {i} is Successful");
                }
                else
                {
                    Console.WriteLine($"Test {i} is Unsuccessful");
                }

                // Clear cookies to reset session for next test
                driver.Manage().Cookies.DeleteAllCookies();
            }
        }
    }
}


