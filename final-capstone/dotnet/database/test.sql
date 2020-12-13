select * from users

SELECT * FROM maintenance_request

SELECT lease_id, property_id, payment_due_date, late_fees FROM transactions



BEGIN TRANSACTION

INSERT INTO properties
(userId, address_id, bedrooms, bathrooms, photo, prop_desc, price)
VALUES
(5, 1, 0, 3, 'https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/suburban-house-royalty-free-image-1584972559.jpg', 'Lovely home just a few minutes away from the shore. With three bedrooms and two bathrooms it is sure to have plenty of space and amenities.', 2200);
ROLLBACK TRANSACTION

SELECT transaction_id,transactions.lease_id, transactions.property_id, payment_due_date, late_fees, paid, amount_paid, lease.userId, properties.price 
FROM transactions
INNER JOIN lease ON lease.lease_id = transactions.lease_id
INNER JOIN properties ON properties.property_id = lease.property_id
WHERE transaction_id = 1;