# FunBooksAndVideos

If having problems with the certificate for localhost in chrome:

Open up Chrome Settings > settings > security > Manage certificates (or go to chrome://certificate-manager/)
Click "Manage imported certificates from Windows" and find the certificate for localhost.
Select it, click "Advanced", check all the boxes and click OK. You may have to restart Chrome.

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