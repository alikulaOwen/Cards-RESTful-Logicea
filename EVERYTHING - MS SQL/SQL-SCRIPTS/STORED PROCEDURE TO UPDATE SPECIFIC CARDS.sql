--STORED PROCEDURE TO UPDATE SPECIFIC CARDS




  
  
CREATE PROCEDURE SP_UpdateSpecificCards  
    @userEmail VARCHAR(255),  
    @cardID VARCHAR(255),  
    @fieldToUpdate VARCHAR(255),  
    @updateValue VARCHAR(255)  
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
     
   IF @fieldToUpdate = 'status'  
   BEGIN  
    UPDATE T_CARDS SET CARD_STATUS = @updateValue WHERE CARD_ID = @cardID  
    SELECT 'Card was updated successfuly' AS Message  
   END  
  
   ELSE IF @fieldToUpdate = 'name'  
   BEGIN  
    UPDATE T_CARDS SET CARD_NAME = @updateValue WHERE CARD_ID = @cardID  
    SELECT 'Card was updated successfuly' AS Message  
   END  
     
   ELSE IF @fieldToUpdate = 'description'  
   BEGIN  
    UPDATE T_CARDS SET CARD_DESCRIPTION = @updateValue WHERE CARD_ID = @cardID  
    SELECT 'Card was updated successfuly' AS Message  
   END  
  
   ELSE IF @fieldToUpdate = 'color'  
   BEGIN  
    UPDATE T_CARDS SET CARD_COLOR = @updateValue WHERE CARD_ID = @cardID  
    SELECT 'Card was updated successfuly' AS Message  
   END  
  
   ELSE  
   BEGIN  
    SELECT 'Update field is invalid' AS Message  
   END  
  
  
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
  
  