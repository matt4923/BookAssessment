# Matt W Assessment Project
Book management API.  
.net core 3.1, C#, SQL 

## Notes
If running this project through VS, it should automatically create the database, modify schema and insert test data.  
The default ConnectionString in the **appsettings.json** file is:

`"ConnectionStrings": {
    "CascadeBooks": "Server=(localdb)\\mssqllocaldb;Database=CascadeBook;Trusted_Connection=True;"
  }`
  
  This can be modified to any server/data source as long as the appropriate credentials are also specified.  **Please do not change the database name**
