CREATE USER [RapidDevStarterApi]
	FOR LOGIN [RapidDevStarterApi]
	WITH DEFAULT_SCHEMA = dbo
GO

ALTER ROLE [db_datareader] ADD MEMBER [RapidDevStarterApi]
GO

ALTER ROLE [db_datawriter] ADD MEMBER [RapidDevStarterApi]
