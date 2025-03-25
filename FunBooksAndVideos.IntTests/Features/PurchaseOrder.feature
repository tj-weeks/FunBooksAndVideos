Feature: PurchaseOrder

Call the purchase order api

@tag1
Scenario: Call the api
	Given a purchase order
	When the purchase order is processed
	Then the response should contain 'something'
