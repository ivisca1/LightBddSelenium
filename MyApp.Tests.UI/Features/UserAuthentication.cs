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
        [Label("SCENARIO-REGISTER-AND-LOGIN")]
        [Scenario]
        public void User_can_register_and_login()
        {
            Runner.RunScenario(
                Given_user_is_on_registration_page,
                When_user_registers_with_valid_credentials,
                Then_user_should_be_logged_in_and_redirected_to_home_page,
                When_user_logs_out_and_logs_back_in,
                Then_user_should_be_logged_in_again_successfully
            );
        }
    }
}
