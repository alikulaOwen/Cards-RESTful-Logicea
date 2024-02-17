--STORTED PROCEDURE USERS LOGIN    



    
CREATE PROCEDURE SP_Login      
    @InputUserEmail VARCHAR(100),      
    @InputPassword VARCHAR(100)      
AS      
BEGIN      
    SET NOCOUNT ON;      
      
	DECLARE @USEREMAIL VARCHAR (100);    
    DECLARE @USERNAME VARCHAR (50);      
	DECLARE @FIRSTNAME VARCHAR (50);      
	DECLARE @LASTNAME VARCHAR (50);      
    DECLARE @ROLE VARCHAR(50);      
      
    -- Check if the provided username and password match a user in the T_USERS table      
    SELECT @USEREMAIL = EMAIL_ADDRESS, @USERNAME = USERNAME, @FIRSTNAME = FIRSTNAME, @LASTNAME = LASTNAME, @ROLE = ROLE      
    FROM T_USERS      
    WHERE EMAIL_ADDRESS = @InputUserEmail AND PASSWORD = @InputPassword;      
      
    -- Return user information or an indication of authentication failure      
    IF (@Username IS NOT NULL)      
    BEGIN      
        -- Successful login      
        SELECT @USEREMAIL AS USEREMAIL, @USERNAME AS USERNAME, @FIRSTNAME AS FIRSTNAME, @LASTNAME AS LASTNAME,       
		@ROLE AS ROLE,'Login successful' AS Message;      
    END      
    ELSE      
    BEGIN      
        -- Failed login      
        SELECT NULL AS USEREMAIL, NULL AS USERNAME, NULL AS FIRSTNAME, NULL AS LASTNAME,       
		NULL AS ROLE,'Invalid username or password' AS Message;      
    END      
END;      
      
      
      