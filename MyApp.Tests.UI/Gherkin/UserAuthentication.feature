Feature: User Authentication
  In order to use the application
  As a new user
  I want to register and log in successfully

  Scenario Outline: Successful user registration and login
    Given the user is on the registration page
    When the user registers with valid credentials
    Then the user should be logged in and redirected to the home page
    When the user logs out and logs back in
    Then the user should be logged in again successfully
  Examples:
    | email                 | password   | expectedResult         |
    | user1@example.com     | Test123!   | Success                |
    | invalid-email         | Test123!   | Registration Failed    |
    | user2@example.com     | short      | Password Too Weak      |