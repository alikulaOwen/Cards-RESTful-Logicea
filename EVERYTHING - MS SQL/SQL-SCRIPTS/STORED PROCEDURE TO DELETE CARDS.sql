--STORED PROCEDURE TO DELETE CARDS  





  
  
  
CREATE PROCEDURE SP_DeleteSpecificCards  
    @userEmail VARCHAR(255),  
    @cardID VARCHAR(255)  
AS  
BEGIN  
 DECLARE @CardCreatedBy NVARCHAR(50)  
    -- Check if the card exists and was created by the specified user  
    IF EXISTS (SELECT 1 FROM T_CARDS WHERE CARD_ID = @cardID)  
    BEGIN  
  -- Get the user's role from T_USERS  
  SELECT @CardCreatedBy = CREATE_BY  
  FROM T_CARDS  
  WHERE CARD_ID = @cardID;  
  
  
  -- Check user's role and retrieve records accordingly  
  IF @CardCreatedBy = @userEmail  
  BEGIN  
   -- If user is an ADMIN, select all records from the table  
   DELETE FROM T_CARDS WHERE CARD_ID = @cardID  
   SELECT 'Card was deleted successfuly' AS Message  
  END  
  ELSE  
  BEGIN  
   -- If user is a MEMBER, select records created by the user_email  
   SELECT 'You do not have access to this card' AS Message  
  END  
    END  
    ELSE  
    BEGIN  
        -- If the card doesn't exist or user doesn't have access  
       SELECT CONCAT(CONCAT('Card: ', @cardID), ', does not exist.')  AS Message  
    END  
END;  
  
  
  