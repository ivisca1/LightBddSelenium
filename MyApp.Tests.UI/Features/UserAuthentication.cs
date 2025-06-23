using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace MyApp.Tests.UI.Features
{
    [Label("FEAT-USER-AUTH")]
    [FeatureDescription(
@"In order to use the application
As a new user
I want to register and log in successfully")]
    public partial class UserAuthentication : FeatureFixture
    {
        [Label("SCENARIO-SUCCESS")]
        [Scenario]
        public void User_can_register_and_login()
        {
            Runner.RunScenario(
                _ => Given_user_is_on_registration_page(),
                _ => When_user_registers_with_email_and_password("user1@example.com", "Test123!"),
                _ => Then_user_should_see("Success"),
                _ => When_user_logs_in_again_with_email_and_password_if_result_is_success(),
                _ => Then_user_should_be_logged_in_again_successfully_if_result_is_success()
            );

        }

        [Label("SCENARIO-INVALID-EMAIL")]
        [Scenario]
        public void User_registration_fails_with_invalid_email()
        {
            Runner.RunScenario(
                _ => Given_user_is_on_registration_page(),
                _ => When_user_registers_with_email_and_password("invalid-email", "Test123!"),
                _ => Then_user_should_see("Invalid email"),
                _ => When_user_logs_in_again_with_email_and_password_if_result_is_success(),
                _ => Then_user_should_be_logged_in_again_successfully_if_result_is_success()
            );
        }

        [Label("SCENARIO-WEAK-PASSWORD")]
        [Scenario]
        public void User_registration_fails_with_weak_password()
        {
            Runner.RunScenario(
                _ => Given_user_is_on_registration_page(),
                _ => When_user_registers_with_email_and_password("user2@example.com", "123"),
                _ => Then_user_should_see("Password Too Weak"),
                _ => When_user_logs_in_again_with_email_and_password_if_result_is_success(),
                _ => Then_user_should_be_logged_in_again_successfully_if_result_is_success()
            );
        }

    }
}
