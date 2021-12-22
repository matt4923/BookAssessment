# Matt W Assessment Project
Book Assessment API for Cascade  
.net 6, C#, SQL 

# Update 12/22/2021
* Implemented NewSortController.  All sorting methods funnel through a single endpoint.  The query string variable indicates the sort method to be applied.
* Entity framework implemented for db creation and CRUD operations
* Crud operations on books

## Testing new update
### new api endpoint: `/book/newsort/<sortInt>`
This allows a user to return a sorted list through a single endpoint (httpGet).  Specify the type of sort desired in the <sortInt> query string.
* .../book/newsort/1 => Returns sorted JSON list by Publisher, Author (last, first), then title (same as Sort1 below)
* .../book/newsort/2 => Returns sorted JSON list by Author (last, first), then title (same as Sort2 below)
* .../book/newsort/3 => Returns sorted JSON list in same format as 1 but retrieved through Stored Procedure
* .../book/newsort/4 => Returns sorted JSON list in same format as 2 but retrieved through Stored Procedure

### CRUD Operations
This assessment now allows for CRUD operations, (performed through Entity Framework).  In order to test these operations, please use postman or a similar API testing application.  For the web request body, you can use the following JSON template:
    `
    {
"Publisher":"pub1",
"Title":"title1",
"AuthorLastName":"LastName1",
"AuthorFirstName":"FirstName1",
"Price":"19.95",
"PublicationYear":"1995",
"Url":"www.amazon.com"
}
    `
* Add new book (HttpPost ~ .../book/) -> Edit the above JSON to denote the properties of the new book.  Send the JSON object in the body of the web request through Postman.
* Update book (HttpPut ~ .../book/\<bookTitle\>) -> Edit the above JSON to denote the updated properties of the book.  Also, indicate the <bookTitle> (title of book you'd like to update) in the url route.  Send the updated JSON object in the body of the web request through Postman.
* Delete book (HttpDelete ~ .../book/\<bookTitle\>) -> Indicate the <bookTitle> (title of book you'd like to update) in the url route.  Send the updated JSON object in the body of the web request through Postman.
 
    
# Previous Instructions
If my interpretation of any instructions are incorrect, I'm happy to modify as needed.

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

In SSMS you can see the database

![image](https://user-images.githubusercontent.com/35410250/145463924-5f1027e5-8108-4930-89af-edb471e2a7c1.png)

#### Note
All of the schema, stored procedures and test data will be added only if the database does NOT exist.  If there are problems running this, the database must be deleted.  If you'd like, I could make this more robust and check for the creation of individual items even if the db exists.

### API Endpoints
* https://localhost:44344/book - API Entry point - (the port may be different depending on how it's running, in these instructions, it's running through IIS Express)
* https://localhost:44344/book/sort1 - Returns sorted JSON list by Publisher, Author (last, first), then title
* https://localhost:44344/book/sort2 - Returns sorted JSON list by Author (last, first), then title
* https://localhost:44344/book/sort3 - Returns sorted JSON list in same format as sort1 but retrieved through Stored Procedure
* https://localhost:44344/book/sort4 - Returns sorted JSON list in same format as sort2 but retrieved through Stored Procedure
* https://localhost:44344/book/total - Returns the sum price of all books in the database


## Instructions and Responses

1.  ##### Create a REST API using ASP.NET MVC and write a method to return a sorted list of these by Publisher, Author (last, first), then title.**
    - See https://localhost:44344/book/sort1 for sorted json data.
3.  ##### Write another API method to return a sorted list by Author (last, first) then title.
    - See https://localhost:44344/book/sort2 for sorted json data.
5.  ##### If you had to create one or more tables to store the Book data in a MS SQL database, outline the table design along with fields and their datatypes.
    - See **_Database Schema** section below
7.  ##### Write stored procedures in MS SQL Server for steps 1 and 2, and use them in separate API methods to return the same results.
    - See https://localhost:44344/book/sort3 and https://localhost:44344/book/sort4 as well as the code behind 
9.  ##### Write an API method to return the total price of all books in the database.
    - See https://localhost:44344/book/total for the sum total of all book prices
11. ##### If you have a large list of these in memory and want to save the entire list to the MS SQL Server database, what is the most efficient way to save the list with only one call to the DB server?
    - See ___Multiple Items, One Call___ section below
13. ##### Add a property to the Book class that outputs the MLA (Modern Language Association) style citation as a string (https://images.app.goo.gl/YkFgbSGiPmie9GgWA). Please add whatever additional properties the Book class needs to generate the citation.
    - Result can be seen in any of the 'sort' calls.  See code behind: 
    
        _BookApi.Models.Book_

        `public string MLACitation => $"{AuthorLastName}, {AuthorFirstName}. \"{Title}\", {Publisher}, {PublicationYear}";`
    
15. ##### Add another property to generate a Chicago style citation (Chicago Manual of Style) (https://images.app.goo.gl/w3SRpg2ZFsXewdAj7).
    - Results can be seen in any of the 'sort' calls.  See code behind:

        _BookApi.Models.Book_
        
        `public string ChicagoCitation => $"{AuthorLastName}, {AuthorFirstName}. {PublicationYear}. \"{Title}.\" {Url}";`
    
## Database Schema
A single table was added with the following columns/Types.  Since it is test data, I made everything required.
- ID (uniqueidentifier, not null) -Primary Key
- Publisher (varchar(50), not null)
- Title (varchar(50), not null)
- AuthorLastName (varchar(20), not null)
- Price (varchar(20), not null)
- PublicationYear (int, not null)
- Url (varchar(100), not null)
## Multiple Items, One Call
1.	On the db server, create a type like the destination table
    
    `CREATE TYPE DestinationType AS Table
     (ColA int,
      ColB int,
      ColC int)`
    
3.	On the db server, create a stored procedure that accepts a _DestinationType_ parameter:

    `CREATE PROC Insert_into_Dest_Table(@dataTable DestinationType)
    AS
    INSERT INTO DestinationTable (ColumnA, ColumnB, ColumnC)
    SELECT ColA, ColB, ColC FROM @dataTable
    GO`

2.	On the web server, convert the list to be saved to a DataTable type that matches the schema of the destination db table. 
3.	On the web server, create make a call to the database server where CommandText is the Stored Procedure execution.  Also add the DataTable (step 2) as a parameter:

    `Using(var conn = new SqlConnection(connString)){
        Using(SqlCommand cmd = conn.CreateCommand()){
            Cmd.CommandText = “EXEC Insert_into_Dest_Table”;
            Conn.Open();
            Cmd.Parameters.AddWithValue(“@dataTable”, destinationDataTable);
            Cmd.ExecuteNonQuery();
        }
    }`

