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
	landlord_id		INT					NOT NULL,
	address_id		INT					NOT NULL,
	userId			INT					NOT NULL,
	bedrooms		INT					NOT NULL,
	bathrooms		INT					NOT NULL,
	photo			VARCHAR(200),
	prop_desc		VARCHAR(500)		NOT NULL,
	price			DECIMAL				NOT NULL,
	CONSTRAINT		PK_propertyId		PRIMARY KEY (property_id),
);

CREATE TABLE property_user (
	property_id		INT		NOT NULL,
	userId			INT		NOT NULL,
	CONSTRAINT		FK_property_to_user	FOREIGN KEY (property_id)	REFERENCES properties (property_id),
	CONSTRAINT		FK_user_to_property	FOREIGN KEY (userId)	REFERENCES users (userId)
);


CREATE TABLE lease (
	lease_id		INT IDENTITY(1,1)	NOT NULL,
	from_date		DATE,
	to_date			DATE,
	userId			INT					NOT NULL,
	property_id		INT					NOT NULL,
	CONSTRAINT		PK_lease_id			PRIMARY KEY (lease_id),
	CONSTRAINT		FK_userId_user		FOREIGN KEY (userId)		REFERENCES users (userId),
	CONSTRAINT		FK_propertyId_prop	FOREIGN KEY (property_id)	REFERENCES properties (property_id)
	);

CREATE TABLE pending_leases (
	pending_id		INT	IDENTITY(1,1)	NOT NULL,
	userId			INT					NOT NULL,
	property_id		INT					NOT NULL,
	from_date		DATE,
	to_date			DATE,
	is_approved		BIT,
	CONSTRAINT		FK_property_id		FOREIGN KEY (property_id)	REFERENCES properties (property_id),
	CONSTRAINT		FK_user_id			FOREIGN KEY (userId)		REFERENCES users (userId),
	CONSTRAINT		FK_pending_id		PRIMARY KEY (pending_id)
	);

CREATE TABLE renter_information ( 
	renter_id			INT IDENTITY(1,1)	NOT NULL,
	userId				INT					NOT NULL,
	first_name			VARCHAR(64)			NOT NULL,
	last_name			VARCHAR(64)			NOT NULL,
	current_address		VARCHAR(64)			NOT NULL,
	phone_number		VARCHAR(10)			NOT NULL,
	email				VARCHAR(300)		NOT NULL,
	lease_type			VARCHAR(64)			NOT NULL,
	salary				DECIMAL				NOT NULL,

	CONSTRAINT		PK_renter_id		PRIMARY KEY (renter_id),
	CONSTRAINT		FKuser_id			FOREIGN KEY (userId)		REFERENCES users (userId),

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



--populate default address data
INSERT INTO address_table
	(userId, property_type, street, street2, city, region, zip) 
VALUES 
	(1, 'House', '42 Wallaby Way', 'N/A', 'Syndey', 'North Carolina', 27009);
INSERT INTO address_table
	(userId, property_type, street, street2, city, region, zip) 
VALUES 
	(2, 'Apt', '500 Smith Rd', 'Building #3 Apt 12', 'San Francisco', 'California', 94016);

--populate default property data
INSERT INTO properties 
	(address_id, landlord_id, userId, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(1, 2, 0, 3, 2, 'https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/suburban-house-royalty-free-image-1584972559.jpg', 'Lovely home just a few minutes away from the shore. With three bedrooms and two bathrooms it is sure to have plenty of space and amenities.', 2200);
INSERT INTO properties 
	(address_id, landlord_id, userId, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(2, 2, 0, 2, 1, 'https://stmedia.stimg.co/1010121201_mavenrendering.jpg?fit=crop&crop=faces', 'Nice apartment with good view and spacing. Perfect for a couple or bachelor.', 1300);


--populate default renter_information data
GO