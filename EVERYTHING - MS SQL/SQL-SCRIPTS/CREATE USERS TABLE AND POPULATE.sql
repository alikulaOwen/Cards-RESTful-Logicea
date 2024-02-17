--CREATE USERS TABLE AND POPULATE IT




CREATE TABLE T_USERS(
	EMAIL_ADDRESS VARCHAR (100) PRIMARY KEY,
	USERNAME VARCHAR (50),
	FIRSTNAME VARCHAR (50),
	LASTNAME VARCHAR (50),
	ROLE VARCHAR (50),
	PASSWORD VARCHAR (100)
)











--INSERT DATA

INSERT INTO T_USERS (
    EMAIL_ADDRESS,
    USERNAME,
    FIRSTNAME,
    LASTNAME,
    ROLE,
    PASSWORD
)
VALUES
    ( 'benjaminsinzore@gmail.com', 'benjaminsinzore', 'Benjamin', 'Sinzore', 'ADMIN', 'PASSWORD_BENJAMIN'),
    ( 'johndoe@gmail.com', 'johndoe', 'John', 'Doe', 'MEMBER', 'PASSWORD_JOHN'),
    ( 'janedoe@gmail.com', 'janedoe', 'Jane', 'Doe', 'MEMBER', 'PASSWORD_JANE');





