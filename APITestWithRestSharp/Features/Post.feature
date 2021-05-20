Feature: Post
	Simple rest sharp API test


Scenario: Simple post Call test
	Given I perform a Get operation on "posts/{postid}"
	And I call the the "1" post
	Then the result should be the "author" as "Vipin Singh" 