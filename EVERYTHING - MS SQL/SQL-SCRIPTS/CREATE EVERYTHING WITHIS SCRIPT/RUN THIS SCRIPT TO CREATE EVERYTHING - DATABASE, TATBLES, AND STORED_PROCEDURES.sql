USE [master]
GO
/****** Object:  Database [db_cards]    Script Date: 17/02/2024 18:31:26 ******/
CREATE DATABASE [db_cards]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_cards', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SINZORE\MSSQL\DATA\db_cards.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db_cards_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SINZORE\MSSQL\DATA\db_cards_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [db_cards] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_cards].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_cards] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_cards] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_cards] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_cards] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_cards] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_cards] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db_cards] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_cards] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_cards] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_cards] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_cards] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_cards] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_cards] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_cards] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_cards] SET  DISABLE_BROKER 
GO
ALTER DATABASE [db_cards] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_cards] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_cards] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_cards] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_cards] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_cards] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_cards] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_cards] SET RECOVERY FULL 
GO
ALTER DATABASE [db_cards] SET  MULTI_USER 
GO
ALTER DATABASE [db_cards] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_cards] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_cards] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_cards] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_cards] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db_cards] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'db_cards', N'ON'
GO
ALTER DATABASE [db_cards] SET QUERY_STORE = ON
GO
ALTER DATABASE [db_cards] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [db_cards]
GO
/****** Object:  User [benjie]    Script Date: 17/02/2024 18:31:26 ******/
CREATE USER [benjie] FOR LOGIN [benjie] WITH DEFAULT_SCHEMA=[benjie]
GO
/****** Object:  Schema [benjie]    Script Date: 17/02/2024 18:31:27 ******/
CREATE SCHEMA [benjie]
GO
/****** Object:  Table [dbo].[T_CARDS]    Script Date: 17/02/2024 18:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_CARDS](
	[CARD_ID] [varchar](100) NOT NULL,
	[CARD_NAME] [varchar](100) NULL,
	[CARD_DESCRIPTION] [varchar](100) NULL,
	[CARD_COLOR] [varchar](100) NULL,
	[CREATE_BY] [varchar](100) NULL,
	[CARD_STATUS] [varchar](50) NULL,
	[CREATED_ON] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[CARD_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_USERS]    Script Date: 17/02/2024 18:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_USERS](
	[EMAIL_ADDRESS] [varchar](100) NOT NULL,
	[USERNAME] [varchar](50) NULL,
	[FIRSTNAME] [varchar](50) NULL,
	[LASTNAME] [varchar](50) NULL,
	[ROLE] [varchar](50) NULL,
	[PASSWORD] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[EMAIL_ADDRESS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_AddCard]    Script Date: 17/02/2024 18:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddCard]    
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



    
    
    
  
  
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteSpecificCards]    Script Date: 17/02/2024 18:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_DeleteSpecificCards]
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



GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllCards]    Script Date: 17/02/2024 18:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAllCards]  
    @userEmail NVARCHAR(255),
    @PageNumber INT,
    @PageSize INT
AS  
BEGIN  
    DECLARE @userRole NVARCHAR(50)  
  
    -- Get the user's role from T_USERS  
    SELECT @userRole = Role  
    FROM T_USERS  
    WHERE EMAIL_ADDRESS = @userEmail;  

	-- Variables for pagination
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    DECLARE @RowCount INT;
  
  
    -- Check user's role and retrieve records accordingly  
    IF @userRole = 'ADMIN'  
    BEGIN  
        -- If user is an ADMIN, select all records from the table  
        SELECT CARD_ID AS CARDID, CARD_NAME AS CARDNAME, CARD_DESCRIPTION AS CARDDESCRIPTION, CARD_COLOR AS CARDCOLOR, CREATE_BY AS CREATEDBY, CARD_STATUS AS CARDSTATUS, CREATED_ON AS CREATEDON  
        
		FROM T_CARDS ORDER BY CREATED_ON DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
    END  
    ELSE IF @userRole = 'MEMBER'  
    BEGIN  
        -- If user is a MEMBER, select records created by the user_email  
        SELECT CARD_ID AS CARDID, CARD_NAME AS CARDNAME, CARD_DESCRIPTION AS CARDDESCRIPTION, CARD_COLOR AS CARDCOLOR, CREATE_BY AS CREATEDBY, CARD_STATUS AS CARDSTATUS, CREATED_ON AS CREATEDON
        
		FROM T_CARDS  
        
		WHERE CREATE_BY = @userEmail ORDER BY CREATED_ON DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY; 
    END   
END;  
  



GO
/****** Object:  StoredProcedure [dbo].[SP_GetSpecificCards]    Script Date: 17/02/2024 18:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetSpecificCards]  
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
  
  
  
  
  
  
GO
/****** Object:  StoredProcedure [dbo].[SP_Login]    Script Date: 17/02/2024 18:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
  
CREATE PROCEDURE [dbo].[SP_Login]    
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
    
    
    
    
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchForCards]    Script Date: 17/02/2024 18:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SearchForCards]  
    @userEmail VARCHAR(255),  
    @searchKey VARCHAR(255),  
    @PageNumber INT,  
    @PageSize INT  
AS  
BEGIN  
    DECLARE @userRole NVARCHAR(50)  
  
    -- Get the user's role from T_USERS  
    SELECT @userRole = Role  
    FROM T_USERS  
    WHERE EMAIL_ADDRESS = @userEmail;  
  
 -- Variables for pagination  
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;  
    DECLARE @RowCount INT;  
  
  
    -- Check user's role and retrieve records accordingly  
    IF @userRole = 'ADMIN'  
    BEGIN  
        -- If user is an ADMIN, select all records from the table  
        SELECT CARD_ID AS CARDID, CARD_NAME AS CARDNAME, CARD_DESCRIPTION AS CARDDESCRIPTION, CARD_COLOR AS CARDCOLOR, CREATE_BY AS CREATEDBY, CARD_STATUS AS CARDSTATUS, CREATED_ON AS CREATEDON   
        FROM T_CARDS WHERE CARD_ID LIKE '%' + @searchKey + '%' OR CARD_NAME LIKE '%' + @searchKey + '%'  
  OR CARD_COLOR LIKE '%' + @searchKey + '%' OR CARD_STATUS LIKE '%' + @searchKey + '%' OR CREATED_ON LIKE '%' + @searchKey + '%'  
  ORDER BY CREATED_ON DESC  
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;  
    END  
    ELSE IF @userRole = 'MEMBER'  
    BEGIN  
        -- If user is a MEMBER, select records created by the user_email  
        SELECT CARD_ID AS CARDID, CARD_NAME AS CARDNAME, CARD_DESCRIPTION AS CARDDESCRIPTION, CARD_COLOR AS CARDCOLOR, CREATE_BY AS CREATEDBY, CARD_STATUS AS CARDSTATUS, CREATED_ON AS CREATEDON   
        FROM T_CARDS  
        WHERE CREATE_BY = @userEmail AND (CARD_ID LIKE '%' + @searchKey + '%' OR CARD_NAME LIKE '%' + @searchKey + '%'  
        OR CARD_COLOR LIKE '%' + @searchKey + '%' OR CARD_STATUS LIKE '%' + @searchKey + '%' OR CREATED_ON LIKE '%' + @searchKey + '%')  
        ORDER BY CREATED_ON DESC  
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;  
    END  
END;  
  
  
  
  
  
  
  
GO
/****** Object:  StoredProcedure [dbo].[sp_test]    Script Date: 17/02/2024 18:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_test]
--()
as
	begin
		select 'hello benjamin'
	end

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateSpecificCards]    Script Date: 17/02/2024 18:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_UpdateSpecificCards]
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


GO
USE [master]
GO
ALTER DATABASE [db_cards] SET  READ_WRITE 
GO
