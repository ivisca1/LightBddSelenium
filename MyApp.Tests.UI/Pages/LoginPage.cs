using OpenQA.Selenium;

public class LoginPage
{
    private readonly IWebDriver _driver;
    private readonly string _url = "https://localhost:7087/Identity/Account/Login";

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Open() => _driver.Navigate().GoToUrl(_url);

    public void FillForm(string email, string password)
    {
        _driver.FindElement(By.Id("Input_Email")).SendKeys(email);
        _driver.FindElement(By.Id("Input_Password")).SendKeys(password);
    }

    public void Submit() => _driver.FindElement(By.Id("login-submit")).Click();
}
