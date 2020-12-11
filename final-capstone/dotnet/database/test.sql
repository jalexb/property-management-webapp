select * from users

SELECT * FROM maintenance_request



BEGIN TRANSACTION

INSERT INTO properties
(userId, address_id, bedrooms, bathrooms, photo, prop_desc, price)
VALUES
(5, 1, 0, 3, 'https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/suburban-house-royalty-free-image-1584972559.jpg', 'Lovely home just a few minutes away from the shore. With three bedrooms and two bathrooms it is sure to have plenty of space and amenities.', 2200);
ROLLBACK TRANSACTION