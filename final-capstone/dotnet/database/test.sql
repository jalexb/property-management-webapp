select * from users

SELECT * FROM maintenance_request

SELECT lease_id, property_id, payment_due_date, late_fees FROM transactions

SELECT * FROM properties INNER JOIN address_table on address_table.address_id = properties.address_id WHERE properties.userId = @landlord_id

SELECT * FROM properties WHERE userId = 5

SELECT * FROM lease

SELECT lease_id, from_date, to_date, userId, property_id FROM lease WHERE property_id = 2 AND (current_status = 'approved' OR current_status = 'Approved') 

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