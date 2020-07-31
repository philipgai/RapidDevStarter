﻿CREATE USER [RapidDevStarterApi]
	FOR LOGIN [RapidDevStarterApi]
	WITH DEFAULT_SCHEMA = dbo
GO

GRANT CONNECT TO [RapidDevStarterApi] WITH GRANT OPTION  AS [dbo]
GO

EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'RapidDevStarterApi';
GO

EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'RapidDevStarterApi';
GO