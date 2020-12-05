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
	user_id			INT IDENTITY(1,1)	NOT NULL,
	username		VARCHAR(50)			NOT NULL,
	password_hash	VARCHAR(200)		NOT NULL,
	salt			VARCHAR(200)		NOT NULL,
	user_role		VARCHAR(50)			NOT NULL
	CONSTRAINT		PK_user				PRIMARY KEY (user_id)
)

CREATE TABLE address_table(
	address_id		INT IDENTITY(1,1)	NOT NULL,
	user_id			INT					NOT NULL,
	property_type	VARCHAR(50)			NOT NULL,
	street			VARCHAR(50)			NOT NULL,
	street2			VARCHAR(50),
	city			VARCHAR(50)			NOT NULL,
	region			VARCHAR(50)			NOT NULL,
	zip				INT					NOT NULL,
	CONSTRAINT		PK_address_id		PRIMARY KEY (address_id),
	CONSTRAINT		FK_user_id			FOREIGN KEY (user_id)		REFERENCES users (user_id)	
)

CREATE TABLE properties (
	property_id		INT IDENTITY(1,1)	NOT NULL,
	address_id		INT					NOT NULL,
	user_id			INT					NOT NULL,
	bedrooms		INT					NOT NULL,
	bathrooms		INT					NOT NULL,
	photo			VARCHAR(200),
	prop_desc		VARCHAR(500)		NOT NULL,
	price			DECIMAL				NOT NULL,
	CONSTRAINT		PK_property_id		PRIMARY KEY (property_id)
)

CREATE TABLE lease (
	lease_id		INT IDENTITY(1,1)	NOT NULL,
	from_date		DATE,
	to_date			DATE,
	user_id			INT					NOT NULL,
	property_id		INT					NOT NULL,
	CONSTRAINT		PK_lease_id			PRIMARY KEY (lease_id),
	CONSTRAINT		FK_user_id			FOREIGN KEY (user_id)		REFERENCES users (user_id),
	CONSTRAINT		FK_property_id		FOREIGN KEY (property_id)	REFERENCES properties (property_id)
	);


ALTER TABLE properties
	ADD			FOREIGN KEY (address_id)	REFERENCES address_table (address_id);


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
	(user_id, property_type, street, street2, city, region, zip) 
VALUES 
	(1, 'House', '42 Wallaby Way', null, 'Syndey', 'North Carolina', 27009);
INSERT INTO address_table
	(user_id, property_type, street, street2, city, region, zip) 
VALUES 
	(2, 'Apt', '500 Smith Rd', 'Building #3 Apt 12', 'San Francisco', 'California', 94016);

--populate default property data
INSERT INTO properties 
	(address_id, user_id, bedrooms, bathrooms, photo, prop_description, price) 
VALUES 
	(1, 1, 3, 2, 'https://photos.google.com/photo/AF1QipNFOOdX72uzUqBmi9ekgSQ0Yen_W4XZqISpHaQb', 'Lovely home just a few minutes away from the shore. With three bedrooms and two bathrooms it is sure to have plenty of space and amenities.', 2200);
INSERT INTO properties 
	(address_id, user_id, bedrooms, bathrooms, photo, prop_description, price) 
VALUES 
	(2, 2, 2, 1, 'https://photos.google.com/photo/AF1QipMZBmYLv_JuARMkiL8kYIXsjwT4DMfLZflfhe0o', 'Nice apartment with good view and spacing. Perfect for a couple or bachelor.', 1300);

GO