USE [RapidDevStarter]
GO

EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'RapidDevStarterApi';
GO

EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'RapidDevStarterApi';
GO