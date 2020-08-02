ALTER TABLE [dbo].[ContactInfo] SET ( SYSTEM_VERSIONING = OFF )
ALTER TABLE [dbo].[User] SET ( SYSTEM_VERSIONING = OFF )
GO

DELETE FROM [History].[dbo_ContactInfo];
DELETE FROM [dbo].[ContactInfo];
GO

DELETE FROM [History].[dbo_User];
DELETE FROM [dbo].[User];
GO