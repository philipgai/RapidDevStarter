
USE [master]
GO

IF NOT EXISTS
    (SELECT name
     FROM sys.database_principals
     WHERE name = 'RapidDevStarterApi')
BEGIN
    CREATE USER [RapidDevStarterApi]
	    FOR LOGIN [RapidDevStarterApi]
	    WITH DEFAULT_SCHEMA = dbo
END
GO

GRANT CONNECT TO [RapidDevStarterApi] WITH GRANT OPTION  AS [dbo]
GO

EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'RapidDevStarterApi';
GO

EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'RapidDevStarterApi';
GO