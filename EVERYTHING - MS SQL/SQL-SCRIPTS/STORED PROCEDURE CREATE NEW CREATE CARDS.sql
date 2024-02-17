--STORED PROCEDURE CREATE NEW CREATE CARDS




CREATE PROCEDURE SP_AddCard      
    @CardName VARCHAR(100),      
    @CardDescription VARCHAR(100),      
    @CardColor VARCHAR(100),      
    @CreateBy VARCHAR(100)      
AS      
    
BEGIN      
    DECLARE @CardID VARCHAR(100);      
	DECLARE @CardStatus VARCHAR(100);      
      
    -- Generate a random ID      
    SET @CardID = CONCAT('CARD_', CAST(NEWID() AS VARCHAR(36)));      
	SET @CardStatus = 'TO DO'    
      
    BEGIN TRY      
        -- Insert data into the T_CARDS table      
        INSERT INTO T_CARDS (CARD_ID, CARD_NAME, CARD_DESCRIPTION, CARD_COLOR, CREATE_BY, CARD_STATUS, CREATED_ON)      
        VALUES (@CardID, @CardName, @CardDescription, @CardColor, @CreateBy, @CardStatus, GETDATE());      
      
        -- Return the inserted data      
        SELECT CARD_ID AS CARDID, CARD_NAME AS CARDNAME, CREATED_ON AS CREATEDON, 'Card was successfully' AS MESSAGE      
        FROM T_CARDS      
        WHERE CARD_ID = @CardID;      
    END TRY      
    BEGIN CATCH      
        -- Handle errors if the insertion fails      
        SELECT NULL AS CARDID, NULL AS CARDNAME, ERROR_MESSAGE() AS MESSAGE;      
    END CATCH      
END;      
  
  
  
      
      
      
    