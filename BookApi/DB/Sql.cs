namespace BookApi.DB {
    public static class Sql {
        public static string DatabaseNames => "SELECT name FROM sys.databases";
        public static string CreateCascadeBookDatabase => $"CREATE DATABASE {Constant.DBNAME}";
        public static string CreateBookTable => $"CREATE TABLE [dbo].[{Constant.BOOKTABLENAME}]( " +
                                                $"[ID] [uniqueidentifier] NOT NULL," +
                                                $"[Publisher] [varchar](50) NOT NULL," +
                                                $"[Title] [varchar](50) NOT NULL," +
                                                $"[AuthorLastName] [varchar](20) NOT NULL," +
                                                $"[AuthorFirstName] [varchar](20) NOT NULL," +
                                                $"[Price] [decimal](4, 2) NOT NULL," +
                                                $"[PublicationYear] [int] NOT NULL," +
                                                $"[Url] [varchar](100) NOT NULL," +
                                                $"CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([ID] ASC)) " +
                                                $"on [PRIMARY]";

        public static string InsertBook => $"INSERT INTO {Constant.BOOKTABLENAME} (ID, Publisher, Title, AuthorLastName, AuthorFirstName, Price, PublicationYear, Url)" +
                $"VALUES(@ID, @Publisher, @Title, @AuthorLastName, @AuthorFirstName, @Price, @PublicationYear, @Url)";


        //sort books by Publisher, Author (last, first), then title
        internal static string SortByPub => "SELECT * FROM BOOK ORDER BY Publisher, AuthorLastName, AuthorFirstName, title";

        //Author (last, first) then title
        internal static string SortByAuth => "SELECT * FROM BOOK ORDER BY AuthorLastName, AuthorFirstName, title";

        internal static string CreateProcSortByPub => $"IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.Sort_Books_by_Publisher')) exec('CREATE PROC Sort_Books_by_Publisher as {SortByPub}')";
        internal static string CreateProcSortByAuth => $"IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.Sort_Books_by_Author')) exec('CREATE PROC Sort_Books_by_Author as {SortByAuth}')";
        public static string ExecuteProcedureSortByPub => "exec Sort_Books_by_Publisher";
        public static string ExecuteProcedureSortByAuth => "exec Sort_Books_by_Author";

        public static string SumBooks => "select sum(price) from book";
    }
}
