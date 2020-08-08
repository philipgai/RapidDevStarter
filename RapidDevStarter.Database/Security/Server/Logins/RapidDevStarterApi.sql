USE [master]
GO
IF NOT EXISTS 
    (SELECT name  
     FROM master.sys.server_principals
     WHERE name = 'RapidDevStarterApi')
BEGIN
    CREATE LOGIN [RapidDevStarterApi] WITH PASSWORD=N'$(RapidDevStarterApiLoginPwd)', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=ON, CHECK_POLICY=ON
END
GO

USE [master]
GO
ALTER LOGIN [RapidDevStarterApi] WITH PASSWORD=N'$(RapidDevStarterApiLoginPwd)'
GO

USE [master]
GO
GRANT CONNECT SQL TO [RapidDevStarterApi] AS [sa]
GO