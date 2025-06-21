using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace MyApp.Tests.UI.Features
{
    public partial class UserAuthentication : IDisposable
    {
        private IWebDriver _driver;
        private string _email;
        private const string Password = "Test123!";

        private RegisterPage _registerPage;
        private LoginPage _loginPage;

        public UserAuthentication()
        {
            var options = new ChromeOptions();
            options.AddArgument("--window-size=1920,1080");
            _driver = new ChromeDriver(options);

            _registerPage = new RegisterPage(_driver);
            _loginPage = new LoginPage(_driver);
        }

        public void Dispose()
        {
            _driver?.Quit();
        }

        private void Given_user_is_on_registration_page()
        {
            _registerPage.Open();
        }

        private void When_user_registers_with_valid_credentials()
        {
            _email = $"user_{Guid.NewGuid()}@example.com";
            _registerPage.FillForm(_email, Password);
            _registerPage.Submit();

            Thread.Sleep(1000);
        }

        private void Then_user_should_be_logged_in_and_redirected_to_home_page()
        {
            _driver.Url.Should().Be("https://localhost:7087/");
            _driver.PageSource.Should().Contain("Logout");
        }

        private void When_user_logs_out_and_logs_back_in()
        {
            _driver.Navigate().GoToUrl("https://localhost:7087/Identity/Account/Logout");
            _driver.FindElement(By.CssSelector("form button[type='submit']")).Click();

            Thread.Sleep(500);

            _loginPage.Open();
            _loginPage.FillForm(_email, Password);
            _loginPage.Submit();

            Thread.Sleep(1000);
        }

        private void Then_user_should_be_logged_in_again_successfully()
        {
            _driver.Url.Should().Be("https://localhost:7087/");
            _driver.PageSource.Should().Contain("Logout");
        }
    }
}
