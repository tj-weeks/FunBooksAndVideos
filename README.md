# FunBooksAndVideos e-commerce shop

Introduction

FunBooksAndVideos is an e-commerce shop where customers can view books and watch online videos. Users can have memberships for the book club, the video club or for both clubs (premium).

Purchase Order

A purchase order can contain products or membership requests. A purchase order has an PO ID, a customer ID and total price. There is an item line in the purchase order per product purchased (product, membership type). One example of a purchase order is the following:

Purchase Order: 3344656
Total: 48.50
Customer: 4567890
Item lines:
• Video "Comprehensive First Aid Training"
• Book "The Girl on the train"
• Book Club Membership

Business Rules

Several business rules are applied when a purchase order is processed. Some of the business rules are shown in this list:
• BR1. If the purchase order contains a membership, it has to be activated in the customer account immediately.
• BR2. If the purchase order contains a physical product a shipping slip has to be generated.

Tasks

• Implement an Object-Oriented model of the system and expose it as REST API(s) utilizing best practices, standards, and guidelines.
• Implement a flexible Purchase Order Processor using good design principles and patterns.
• Implement the above business rules.

# Example post body for purchase order:

{
    "id": 1,
    "customerId": 1,
    "totalPrice": 100,
    "purchaseItems": [
        {
            "type": "ProductItem",
            "id": 1,
            "name": "Product 1",
            "price": 50
        },
        {
            "type": "MembershipItem",
            "id": 2,
            "name": "Membership 1",
            "price": 50
        }
    ]
}
