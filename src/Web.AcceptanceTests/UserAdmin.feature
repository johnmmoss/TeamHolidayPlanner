Feature: UserAdmin
	In order to use the site
	As an employee
	I want to be able to register and login to the site

Scenario: A valid user can login to the site
	Given I am a valid user of the site
	And I have navigated to the login page
	And I have entered a username of "johnmmoss" and a password of "Password1!"
	When I click the create button
	Then I am logged in to the site
