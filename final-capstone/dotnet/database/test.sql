select * from users



BEGIN TRANSACTION
INSERT INTO pending_leases(userId, property_id, from_date, to_date) VALUES(1, 1, '10-10-10', '10-10-11')

ROLLBACK TRANSACTION