# Matt W Assessment Project
Book management API.  
.net core 3.1, C#, SQL 

## Notes
If running this project through VS, it should automatically create the database, modify schema and insert test data.  
The default ConnectionString in the **appsettings.json** file is:

`"ConnectionStrings": {
    "CascadeBooks": "Server=(localdb)\\mssqllocaldb;Database=CascadeBook;Trusted_Connection=True;"
  }`
  
  This can be modified to any server/data source as long as the appropriate credentials are also specified and the user has full SQL privledges, (create db, tables, stored procedures, insert data, query tables, etc)
  
**Please do not change the database name in the connection string**
## Testing
Ensure project is running in VS and the browser routes to:

https://localhost:44344/book

![image](https://user-images.githubusercontent.com/35410250/145463287-1f89b101-3d2d-478b-8cf2-d8ae8641dac4.png)
This is your indication the database has been created, schema has been added, stored procedures have been built.

![image](https://user-images.githubusercontent.com/35410250/145463924-5f1027e5-8108-4930-89af-edb471e2a7c1.png)

### API Endpoints
* https://localhost:44344/book - API Entry point - (the port may be different depending on how it's running, in these instructions, it's running through IIS Express)
* https://localhost:44344/book/sort1 - Returns sorted JSON list by Publisher, Author (last, first), then title
* https://localhost:44344/book/sort2 - Returns sorted JSON list by Author (last, first), then title
* https://localhost:44344/book/sort3 - Returns sorted JSON list in same format as sort1 but retrieved through Stored Procedure
* https://localhost:44344/book/sort4 - Returns sorted JSON list in same format as sort2 but retrieved through Stored Procedure
* https://localhost:44344/book/total - Returns the sum price of all books in the database


## Instructions and Responses
If my interpretation of the instructions is incorrect, I'm happy to modify as needed.

1.  ##### Create a REST API using ASP.NET MVC and write a method to return a sorted list of these by Publisher, Author (last, first), then title.**
    - 
3.  Write another API method to return a sorted list by Author (last, first) then title.
4.  If you had to create one or more tables to store the Book data in a MS SQL database, outline the table design along with fields and their datatypes.
5.  Write stored procedures in MS SQL Server for steps 1 and 2, and use them in separate API methods to return the same results.
6.  Write an API method to return the total price of all books in the database.
7.  If you have a large list of these in memory and want to save the entire list to the MS SQL Server database, what is the most efficient way to save the list with only one call to the DB server?
8.  Add a property to the Book class that outputs the MLA (Modern Language Association) style citation as a string (https://images.app.goo.gl/YkFgbSGiPmie9GgWA). Please add whatever additional properties the Book class needs to generate the citation.
9.  Add another property to generate a Chicago style citation (Chicago Manual of Style) (https://images.app.goo.gl/w3SRpg2ZFsXewdAj7).
