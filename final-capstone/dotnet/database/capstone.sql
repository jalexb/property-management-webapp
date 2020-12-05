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
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE address_table(

	address_id int IDENTITY(1,1) NOT NULL,
	property_type varchar(50) NOT NULL,
	street varchar(50) NOT NULL,
	street2 varchar(50),
	city varchar(50) NOT NULL,
	region varchar(50) NOT NULL,
	zip int NOT NULL,
	CONSTRAINT PK_address_id PRIMARY KEY (address_id),

)

CREATE TABLE properties (

	property_id int IDENTITY(1,1) NOT NULL,
	address_id int NOT NULL,
	user_id int NOT NULL,
	bedrooms int NOT NULL,
	bathrooms int NOT NULL,
	photo varchar(200),
	prop_description varchar(500) NOT NULL,
	price decimal NOT NULL,
	CONSTRAINT PK_property_id PRIMARY KEY (property_id),
	CONSTRAINT FK_address_id FOREIGN KEY (address_id) REFERENCES address_table (address_id)
)


--populate default user data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

--populate default address data
INSERT INTO address_table(property_type, street, street2, city, region, zip) VALUES ('House', '42 Wallaby Way', null, 'Syndey', 'North Carolina', 27009);
INSERT INTO address_table(property_type, street, street2, city, region, zip) VALUES ('Apt', '500 Smith Rd', 'Building #3 Apt 12', 'San Francisco', 'California', 94016);

--populate default property data
INSERT INTO properties (address_id, user_id, bedrooms, bathrooms, photo, prop_description, price) VALUES (1, 1, 3, 2, 'https://photos.google.com/photo/AF1QipNFOOdX72uzUqBmi9ekgSQ0Yen_W4XZqISpHaQb', 'Lovely home just a few minutes away from the shore. With three bedrooms and two bathrooms it is sure to have plenty of space and amenities.', 2200);
INSERT INTO properties (address_id, user_id, bedrooms, bathrooms, photo, prop_description, price) VALUES (2, 2, 2, 1, 'https://photos.google.com/photo/AF1QipMZBmYLv_JuARMkiL8kYIXsjwT4DMfLZflfhe0o', 'Nice apartment with good view and spacing. Perfect for a couple or bachelor.', 1300);

GO