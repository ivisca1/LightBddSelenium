using OpenQA.Selenium;

public class RegisterPage
{
    private readonly IWebDriver _driver;
    private readonly string _url = "https://localhost:7087/Identity/Account/Register";

    public RegisterPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Open() => _driver.Navigate().GoToUrl(_url);

    public void FillForm(string email, string password)
    {
        _driver.FindElement(By.Id("Input_Email")).SendKeys(email);
        _driver.FindElement(By.Id("Input_Password")).SendKeys(password);
        _driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys(password);
    }

    public void Submit() => _driver.FindElement(By.Id("registerSubmit")).Click();
}
