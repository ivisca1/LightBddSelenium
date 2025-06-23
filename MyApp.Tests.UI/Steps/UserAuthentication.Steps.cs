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
        private string _password;
        private string _expectedResult;

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

        private void When_user_registers_with_email_and_password(string email, string password)
        {
            _email = email;
            _password = password;
            _registerPage.FillForm(_email, _password);
            _registerPage.Submit();

            Thread.Sleep(1000);
        }

        private void Then_user_should_see(string expectedResult)
        {
            _expectedResult = expectedResult;

            switch (expectedResult)
            {
                case "Success":
                    _driver.PageSource.Should().Contain("Logout");
                    break;
                case "Invalid email":
                    _driver.PageSource.Should().Contain("The Email field is not a valid e-mail address.");
                    break;
                case "Password Too Weak":
                    _driver.PageSource.Should().Contain("The Password must be at least 6 and at max 100 characters long.");
                    break;
                default:
                    throw new ArgumentException($"Unknown expected result: {expectedResult}");
            }
        }

        private void When_user_logs_in_again_with_email_and_password_if_result_is_success()
        {
            if (_expectedResult != "Success")
                return;

            _driver.Navigate().GoToUrl("https://localhost:7087/Identity/Account/Logout");
            _driver.FindElement(By.CssSelector("form button[type='submit']")).Click();

            Thread.Sleep(500);

            _loginPage.Open();
            _loginPage.FillForm(_email, _password);
            _loginPage.Submit();

            Thread.Sleep(1000);
        }

        private void Then_user_should_be_logged_in_again_successfully_if_result_is_success()
        {
            if (_expectedResult != "Success")
                return;

            _driver.Url.Should().Be("https://localhost:7087/");
            _driver.PageSource.Should().Contain("Logout");
        }
    }
}
