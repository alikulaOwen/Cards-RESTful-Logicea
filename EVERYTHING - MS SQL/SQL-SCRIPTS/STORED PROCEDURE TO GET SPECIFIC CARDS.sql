--STORED PROCEDURE TO GET SPECIFIC CARDS





CREATE PROCEDURE SP_GetSpecificCards  
    @userEmail VARCHAR(255),  
    @cardID VARCHAR(255)  
AS  
BEGIN  
    DECLARE @userRole NVARCHAR(50)  
  
    -- Get the user's role from T_USERS  
    SELECT @userRole = Role  
    FROM T_USERS  
    WHERE EMAIL_ADDRESS = @userEmail;  
  
  
    -- Check user's role and retrieve records accordingly  
    IF @userRole = 'ADMIN'  
    BEGIN  
        -- If user is an ADMIN, select all records from the table  
        SELECT CARD_ID AS CARDID, CARD_NAME AS CARDNAME, CARD_DESCRIPTION AS CARDDESCRIPTION, CARD_COLOR AS CARDCOLOR, 
		CREATE_BY AS CREATEDBY, CARD_STATUS AS CARDSTATUS, CREATED_ON AS CREATEDON    

        FROM T_CARDS WHERE CARD_ID = @cardID;  
    END  
    ELSE IF @userRole = 'MEMBER'  
    BEGIN  
        -- If user is a MEMBER, select records created by the user_email  
        SELECT CARD_ID AS CARDID, CARD_NAME AS CARDNAME, CARD_DESCRIPTION AS CARDDESCRIPTION, CARD_COLOR AS CARDCOLOR, 
		CREATE_BY AS CREATEDBY, CARD_STATUS AS CARDSTATUS, CREATED_ON AS CREATEDON  
		
        FROM T_CARDS  
        WHERE CREATE_BY = @userEmail AND CARD_ID = @cardID; 
    END   
END;  
  
  
  
  
  
  