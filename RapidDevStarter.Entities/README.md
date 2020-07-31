# RapidDevStarter.Entities

## Generating models and DBContexts (Database-first)
1. Requirements:
   - Install `Microsoft.EntityFrameworkCore.Design`
   - Install `Microsoft.EntityFrameworkCore.Tools`
   - Install `Microsoft.EntityFrameworkCore.SqlServer`
1. Tools > Nuget Package Manager > Package Manager Console
1. `Scaffold-DbContext "Server=localhost;Database=RapidDevStarter;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir RapidDevStarterEntities -Project RapidDevStarter.Entities -StartupProject RapidDevStarter.Entities -Context RapidDevStarterDbContext -Force`