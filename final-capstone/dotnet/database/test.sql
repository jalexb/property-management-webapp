select * from users

SELECT * FROM maintenance_request

SELECT lease_id, property_id, payment_due_date, late_fees FROM transactions

SELECT * FROM properties INNER JOIN address_table on address_table.address_id = properties.address_id WHERE properties.userId = @landlord_id

SELECT * FROM properties WHERE userId = 5

SELECT * FROM lease
SELECT * FROM maintenance_request

UPDATE maintenance_request SET post_fix_report = '' WHERE  request_id = 4

SELECT request_id, renter_id, worker_id, request_info, maintenance_request.property_id, 
                                                    is_assigned, is_fixed, post_fix_report, address_table.street, address_table.street2 
                                                    FROM maintenance_request
                                                    INNER JOIN properties ON properties.property_id = maintenance_request.property_id 
                                                    INNER JOIN address_table ON address_table.address_id = properties.address_id
                                                    WHERE worker_id = 6 AND is_assigned = 1

UPDATE lease SET current_status = 'rejected' WHERE ((userId = 6 AND NOT property_id = 3 ) OR (property_id = 3 AND NOT userId = 6) AND current_status != 'approved')

UPDATE lease SET current_status = 'pending' WHERE userId = 6


SELECT lease_id, from_date, to_date, userId, property_id FROM lease WHERE property_id = 2 AND (current_status = 'approved' OR current_status = 'Approved') 

BEGIN TRANSACTION

UPDATE lease SET current_status = 'rejected' WHERE ((property_id = 3 AND NOT userId = 6 AND current_status != ('approved')) OR (userId = 6 AND NOT property_id = 3 AND current_status != ('approved')));
ROLLBACK TRANSACTION

SELECT transaction_id,transactions.lease_id, transactions.property_id, payment_due_date, late_fees, paid, amount_paid, lease.userId, properties.price 
FROM transactions
INNER JOIN lease ON lease.lease_id = transactions.lease_id
INNER JOIN properties ON properties.property_id = lease.property_id
WHERE transaction_id = 1;