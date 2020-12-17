USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	userId			INT IDENTITY(1,1)	NOT NULL,
	username		VARCHAR(50)			NOT NULL,
	password_hash	VARCHAR(200)		NOT NULL,
	salt			VARCHAR(200)		NOT NULL,
	user_role		VARCHAR(50)			NOT NULL
	CONSTRAINT		PK_user				PRIMARY KEY (userId)
);

CREATE TABLE address_table(
	address_id		INT IDENTITY(1,1)	NOT NULL,
	userId			INT					NOT NULL,
	property_type	VARCHAR(50)			NOT NULL,
	street			VARCHAR(50)			NOT NULL,
	street2			VARCHAR(50)			NOT NULL,
	city			VARCHAR(50)			NOT NULL,
	region			VARCHAR(50)			NOT NULL,
	zip				INT					NOT NULL,
	CONSTRAINT		PK_address_id		PRIMARY KEY (address_id),
	CONSTRAINT		FK_userId_users		FOREIGN KEY (userId)		REFERENCES users (userId)	
);

CREATE TABLE properties (
	property_id		INT IDENTITY(1,1)	NOT NULL,
	userId			INT					NOT NULL,
	address_id		INT					NOT NULL,
	bedrooms		INT					NOT NULL,
	bathrooms		INT					NOT NULL,
	photo			VARCHAR(500),
	prop_desc		VARCHAR(500)		NOT NULL,
	price			DECIMAL				NOT NULL,
	CONSTRAINT		PK_propertyId		PRIMARY KEY (property_id),
	CONSTRAINT		FK_prop_user	FOREIGN KEY (userId)		REFERENCES users (userId)
);

CREATE TABLE lease (
	lease_id		INT IDENTITY(1,1)	NOT NULL,
	from_date		DATE,
	to_date			DATE,
	userId			INT					NOT NULL,
	property_id		INT					NOT NULL,
	current_status  VARCHAR(50)			NOT NULL,
	CONSTRAINT		PK_lease_id			PRIMARY KEY (lease_id),
	CONSTRAINT		FK_userId_user		FOREIGN KEY (userId)		REFERENCES users (userId),
	CONSTRAINT		FK_propertyId_prop	FOREIGN KEY (property_id)	REFERENCES properties (property_id)
	);

CREATE TABLE renter_information ( 
	renter_id			INT IDENTITY(1,1)	NOT NULL,
	userId				INT					NOT NULL,
	first_name			VARCHAR(64)			NOT NULL,
	last_name			VARCHAR(64)			NOT NULL,
	phone_number		VARCHAR(20)			NOT NULL,
	email				VARCHAR(300)		NOT NULL,
	lease_type			VARCHAR(64)			NOT NULL,
	salary				DECIMAL				NOT NULL,

	CONSTRAINT		PK_renter_id		PRIMARY KEY (renter_id),
	CONSTRAINT		FKuser_id			FOREIGN KEY (userId)		REFERENCES users (userId),
	);

CREATE TABLE maintenance_request (
	request_id			INT IDENTITY(1, 1)	NOT NULL,
	renter_id			INT					NOT NULL,
	worker_id			INT,
	request_info		VARCHAR(500)		NOT NULL,
	property_id			INT					NOT NULL,
	is_assigned			BIT,
	is_fixed			BIT,
	post_fix_report		VARCHAR(500),

	CONSTRAINT		PK_request_id	PRIMARY KEY(request_id),
	CONSTRAINT		FK_renter		FOREIGN KEY(renter_id) REFERENCES users (userId),
	CONSTRAINT		FK_worker		FOREIGN KEY(worker_id) REFERENCES users (userId),
	CONSTRAINT		FK_property		FOREIGN KEY(property_id) REFERENCES properties (property_id)
);

CREATE TABLE worker_information ( 
	userId				INT					NOT NULL,
	first_name			VARCHAR(64)			NOT NULL,
	last_name			VARCHAR(64)			NOT NULL,
	phone_number		VARCHAR(20)			NOT NULL,
	email				VARCHAR(300)		NOT NULL,

	CONSTRAINT		PK_worker_id		PRIMARY KEY (userId),
	CONSTRAINT		FK_worker_user_id	FOREIGN KEY (userId)		REFERENCES users (userId),
	);


CREATE TABLE transactions (
	transaction_id		INT IDENTITY(1, 1)  NOT NULL,
	lease_id			INT					NOT NULL,
	property_id			INT					NOT NULL,
	payment_due_date	DATE				NOT NULL,
	late_fees			DECIMAL				NOT NULL,
	paid				BIT					NOT NULL,
	amount_paid			DECIMAL				NOT NULL

	CONSTRAINT		PK_transaction_id	PRIMARY KEY(transaction_id),
	CONSTRAINT		FK_lease_id_		FOREIGN KEY(lease_id) REFERENCES lease (lease_id),
	CONSTRAINT		FK_property_id_trans FOREIGN KEY(property_id) REFERENCES properties (property_id)
);

ALTER TABLE properties
	ADD	CONSTRAINT	FK_addr_id	
	FOREIGN KEY (address_id)			
	REFERENCES address_table (address_id);


--populate default user data
INSERT INTO users 
	(username,	password_hash, salt, user_role) 
VALUES 
	('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');

INSERT INTO users 
	(username, password_hash, salt, user_role) 
VALUES 
	('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
INSERT INTO users (username, password_hash, salt, user_role)
VALUES
	('renter','nIXPgkurH3MTLSqSnUuiyIRVdIo=', '6BR2lRBjrdo=', 'renter');

INSERT INTO users (username, password_hash, salt, user_role)
VALUES
	('worker','nIXPgkurH3MTLSqSnUuiyIRVdIo=', '6BR2lRBjrdo=', 'maintenance'); -- password is a

INSERT INTO users (username, password_hash, salt, user_role)
VALUES
	('landlord','nIXPgkurH3MTLSqSnUuiyIRVdIo=', '6BR2lRBjrdo=', 'landlord');

INSERT INTO users (username, password_hash, salt, user_role)
VALUES
	('Bob','nIXPgkurH3MTLSqSnUuiyIRVdIo=', '6BR2lRBjrdo=', 'maintenance');




--populate default address data
INSERT INTO address_table
	(userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'House', '42 Wallaby Way', 'N/A', 'Syndey', 'North Carolina', 27009); --renter is userId 4
INSERT INTO address_table
	(userId, property_type, street, street2, city, region, zip) 
VALUES 
	(1 , 'Apt', '500 Smith Rd', 'Building #3 Apt 12', 'San Francisco', 'California', 94016);




--populate default property data
INSERT INTO properties 
	(userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 1, 0, 3, 'https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/suburban-house-royalty-free-image-1584972559.jpg', 'Lovely home just a few minutes away from the shore. With three bedrooms and two bathrooms it is sure to have plenty of space and amenities.', 2200);
INSERT INTO properties 
	(userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 2, 0, 2, 'https://stmedia.stimg.co/1010121201_mavenrendering.jpg?fit=crop&crop=faces', 'Nice apartment with good view and spacing. Perfect for a couple or bachelor.', 1300);


--populate default renter_information data

INSERT INTO	renter_information 
	(userId, first_name, last_name, lease_type, phone_number, salary, email)
VALUES 
	(3, 'B', 'G', '1 year', '4444444444', '100000', 'bg@bg.com');
INSERT INTO lease
	(current_status, from_date, property_id, to_date, userId)
VALUES
	('Approved', '10-10-2020', 2, '10-10-2021', 3);


INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'House', '75490 Nantucket Drive', 'N/A', 'Orlando', 'Florida', 32830);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 3, 4, 3, 'https://media.istockphoto.com/photos/beautiful-luxury-home-exterior-at-twilight-picture-id1026205392?k=6&m=1026205392&s=612x612&w=0&h=pe0Pqbm7GKHl7cmEjf9Drc7Fp-JwJ6aTywsGfm5eQm4=', 'Luxury home with 4 bedrooms and 3 bathrooms.  Great for entertaining!', 3200);

INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'Apartment', '2800 Madison Rd', 'Suite 802', 'Cincinnati', 'OH', 45208);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 4, 2, 2, 'https://images.pexels.com/photos/439391/pexels-photo-439391.jpeg?cs=srgb&dl=pexels-sevenstorm-juhaszimrus-439391.jpg&fm=jpg', 'Rooftop terraces, 2 Bedrooms, 2 Bathrooms and 2 car garage, close to shopping and downtown', 1800);

INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'Apartment', '2800 Madison Rd', 'Suite 201', 'Cincinnati', 'OH', 45208);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 5, 2, 2, 'https://images.pexels.com/photos/439391/pexels-photo-439391.jpeg?cs=srgb&dl=pexels-sevenstorm-juhaszimrus-439391.jpg&fm=jpg', 'Rooftop terraces, 2 Bedrooms, 2 Bathrooms and 2 car garage, close to shopping and downtown', 1800);
	
INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'Apartment', '2800 Madison Rd', '205', 'Cincinnati', 'OH', 45208);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 6, 1, 1, 'https://images.pexels.com/photos/439391/pexels-photo-439391.jpeg?cs=srgb&dl=pexels-sevenstorm-juhaszimrus-439391.jpg&fm=jpg', 'Upscale living in Downtown! Close to stadiums and museums.  On-site gym and pool.  Call for an appointment. ', 2200);

INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'Apartment', '2800 Madison Rd', '404', 'Cincinnati', 'OH', 45208);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 7, 3, 2, 'https://images.pexels.com/photos/439391/pexels-photo-439391.jpeg?cs=srgb&dl=pexels-sevenstorm-juhaszimrus-439391.jpg&fm=jpg', 'Rooftop terraces, 2 Bedrooms, 2 Bathrooms and 2 car garage, close to shopping and downtown', 3500);

	INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'Apartment', '2800 Madison Rd', 'Suite 703', 'Cincinnati', 'OH', 45208);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 8, 2, 1, 'https://images.pexels.com/photos/439391/pexels-photo-439391.jpeg?cs=srgb&dl=pexels-sevenstorm-juhaszimrus-439391.jpg&fm=jpg', 'Rooftop terraces, 2 Bedrooms, 2 Bathrooms and 2 car garage, close to shopping and downtown', 2450);

INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'Apartment', '2800 Madison Rd', 'Suite 300', 'Cincinnati', 'OH', 45208);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 9, 3, 1, 'https://images.pexels.com/photos/439391/pexels-photo-439391.jpeg?cs=srgb&dl=pexels-sevenstorm-juhaszimrus-439391.jpg&fm=jpg', 'Rooftop terraces, 2 Bedrooms, 2 Bathrooms and 2 car garage, close to shopping and downtown', 3450);

INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'House', '507 Mountain View Ln', 'N/A', 'Vergennes', 'Vermont', 05491);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 10, 2, 2, 'https://media.istockphoto.com/photos/winter-snow-craftman-cape-cod-style-home-picture-id162264365?k=6&m=162264365&s=612x612&w=0&h=bkOPRPuo9OR7JlIYJzDw0U0QNDBh-bIIWGOYb_O9Z0Q=', 'Quiet country living nestled in northern VT.  Close to Otter Creek', 4000);

INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'House', '37942 Eagle Dr', 'N/A', 'Gatlinburg', 'Tennessee', 37738);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 11, 2, 2, 'https://images.pexels.com/photos/1876045/pexels-photo-1876045.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500', 'Peaceful mountain retreat with beautiful sunrises and sunsets.  Come smell the fresh mountain air while drinking your morning coffee!  Great for entertaining!', 4000);

INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'House', '37942 Eagle Dr', 'N/A', 'Gatlinburg', 'Tennessee', 37738);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 12, 2, 2, 'https://images.pexels.com/photos/2098405/pexels-photo-2098405.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500', 'Lakeside living!  Dock your sailboat right in front of your house.  Enjoy the sounds of nature while using the hot tub!', 3800);

INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'House', '9724 Rt 458', 'N/A', 'Goshen', 'OH', 45122);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 13, 2, 2, 'https://images.pexels.com/photos/463734/pexels-photo-463734.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500', 'This home offers 2 bedrooms and 2 bathrooms, updated kitchen and family room.  Lots of room to expand.', 2600);


INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'House', '37 Bramble Rd', 'N/A', 'Kalamzazoo', 'MI', 49009);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 14, 2, 1, 'https://images.pexels.com/photos/1123876/pexels-photo-1123876.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500', 'Fixer Upper!  Make this home to your own liking.', 600);


INSERT INTO address_table (userId, property_type, street, street2, city, region, zip) 
VALUES 
	(3, 'Tiny House', '6626 Britton Ave', 'N/A', 'Cincinnati', 'OH', 45227);

INSERT INTO properties (userId, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(5, 15, 1, 1, 'https://thumbs.dreamstime.com/b/sumer-rain-over-tiny-house-summer-flowers-around-off-grid-83118571.jpg', 'Tiny home living at it best!  Downsize to the minimum and enjoy all the comforts while not using a lot real estate!', 800);


--populate default transaction data
INSERT INTO transactions (lease_id, property_id, payment_due_date, late_fees, paid, amount_paid)
VALUES (1, 2, '01-01-2021', 0, 0, 0);

INSERT INTO transactions (lease_id, property_id, payment_due_date, late_fees, paid, amount_paid)
VALUES (1, 2, '02-01-2021', 0, 0, 0);

INSERT INTO transactions (lease_id, property_id, payment_due_date, late_fees, paid, amount_paid)
VALUES (1, 2, '03-01-2021', 0, 0, 0);

--populate default maintenance ticket data
INSERT INTO maintenance_request (renter_id, worker_id, request_info, property_id, is_assigned, is_fixed, post_fix_report)
VALUES(3, 4, 'The shower only gets cold water.', 8, 1, 0, '');

INSERT INTO maintenance_request (renter_id, worker_id, request_info, property_id, is_assigned, is_fixed, post_fix_report)
VALUES(3, 4, 'The garbage disposal for the kitchen sink doesn''t work.', 9, 1, 0, '');

INSERT INTO maintenance_request (renter_id, worker_id, request_info, property_id, is_assigned, is_fixed, post_fix_report)
VALUES(3, null, 'My basement flooded', 9, 1, 0, '');

INSERT INTO worker_information (userId, first_name, last_name, phone_number, email)
VALUES(5, 'Bob', 'theBuilder', '513-555-1212', 'BobtheBuilder@wefixthings.com');

GO


