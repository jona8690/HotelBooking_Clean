Feature: Booking
	In order to ensure people cant book rooms when already occuied.
	Our hotel is fully booked between 10 and 20 days in the future.

Scenario: Start date after, end date after
	Given That I check in 21 days
	And That I check out in 25
	When i try to book
	The booking manager should return a room id

Scenario: Start date befre, end date before
	Given That I check in 2 days
	And That I check out in 7
	When i try to book
	The booking manager should return a room id

Scenario: Start date occupied, end date before
	Given That I check in 15 days
	And That I check out in 7
	When i try to book
	The booking manager should return -1

Scenario: Start date before, end date occupied
	Given That I check in 7 days
	And That I check out in 15
	When i try to book
	The booking manager should return -1

Scenario: Start date after, end date occupied
	Given That I check in 22 days
	And That I check out in 15
	When i try to book
	The booking manager should return -1

Scenario: Start date before, end date after
	Given That I check in 7 days
	And That I check out in 22
	When i try to book
	The booking manager should return -1