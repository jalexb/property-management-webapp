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
	user_role		VARCHAR(50)			NOT NULL,
	first_name			VARCHAR(64)			 NULL,
	last_name			VARCHAR(64)			 NULL,
	phone_number		VARCHAR(10)			 NULL,
	email				VARCHAR(300)		 NULL,
	salary				DECIMAL				 NULL,
	address_id		INT					 NULL,
	CONSTRAINT		PK_user				PRIMARY KEY (userId)
	CONSTRAINT		FK_user_address	FOREIGN KEY (address_id)		REFERENCES address_table (address_id)
);

CREATE TABLE address_table(
	address_id		INT IDENTITY(1,1)	NOT NULL,
	property_type	VARCHAR(50)			NOT NULL,
	street			VARCHAR(50)			NOT NULL,
	street2			VARCHAR(50)			NOT NULL,
	city			VARCHAR(50)			NOT NULL,
	region			VARCHAR(50)			NOT NULL,
	zip				INT					NOT NULL,
	CONSTRAINT		PK_address_id		PRIMARY KEY (address_id),
	
);


CREATE TABLE properties (
	property_id		INT IDENTITY(1,1)	NOT NULL,
	landlord_id		INT					NOT NULL,
	address_id		INT					NOT NULL,
	bedrooms		INT					NOT NULL,
	bathrooms		INT					NOT NULL,
	photo			VARCHAR(200),
	prop_desc		VARCHAR(500)		NOT NULL,
	price			DECIMAL				NOT NULL,
	CONSTRAINT		PK_propertyId		PRIMARY KEY (property_id),
	CONSTRAINT		FK_prop_landlord	FOREIGN KEY (landlord_id)		REFERENCES Users (userId)
	CONSTRAINT		FK_prop_address	FOREIGN KEY (address_id)		REFERENCES address_table (address_id)
);

CREATE TABLE lease (
	lease_id		INT IDENTITY(1,1)	NOT NULL,
	from_date		DATE,
	to_date			DATE,
	lease_type			VARCHAR(64)			NOT NULL,
	userId			INT					NOT NULL,
	property_id		INT					NOT NULL,
	current_status  VARCHAR(50)			NOT NULL,
	CONSTRAINT		PK_lease_id			PRIMARY KEY (lease_id),
	CONSTRAINT		FK_userId_user		FOREIGN KEY (userId)		REFERENCES users (userId),
	CONSTRAINT		FK_propertyId_prop	FOREIGN KEY (property_id)	REFERENCES properties (property_id)
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
	('r','nIXPgkurH3MTLSqSnUuiyIRVdIo=', '6BR2lRBjrdo=', 'renter');

INSERT INTO users (username, password_hash, salt, user_role)
VALUES
	('m','nIXPgkurH3MTLSqSnUuiyIRVdIo=', '6BR2lRBjrdo=', 'maintenance'); -- password is a

INSERT INTO users (username, password_hash, salt, user_role)
VALUES
	('l','nIXPgkurH3MTLSqSnUuiyIRVdIo=', '6BR2lRBjrdo=', 'landlord');

--populate false landlord data
INSERT INTO landlord
	(userId)
VALUES
	(5);



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
	(landlord_id, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(1, 1, 0, 3, 'https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/suburban-house-royalty-free-image-1584972559.jpg', 'Lovely home just a few minutes away from the shore. With three bedrooms and two bathrooms it is sure to have plenty of space and amenities.', 2200);
INSERT INTO properties 
	(landlord_id, address_id, bedrooms, bathrooms, photo, prop_desc, price) 
VALUES 
	(1, 2, 0, 2, 'https://stmedia.stimg.co/1010121201_mavenrendering.jpg?fit=crop&crop=faces', 'Nice apartment with good view and spacing. Perfect for a couple or bachelor.', 1300);


--populate default renter_information data
GO