using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlServer.LocalDb;
using SqlServer.LocalDb.Extensions;
using SqlServer.LocalDb.Models;

namespace Testing
{
    [TestClass]
    public class LocalDbTest
    {
        [TestMethod]
        public void TryLocalConnection()
        {
            LocalDb.TryDropDatabase("hello", out _);

            using (var cn = LocalDb.GetConnection("hello"))
            {
                // if this doesn't throw exception, then it works
            }

            LocalDb.TryDropDatabase("hello", out _);
        }

        [TestMethod]
        public void TryDropDbIfExists()
        {
            bool droppedNotExisting = false;
            bool droppedExisting = false;

            // db doesn't exist
            if (LocalDb.TryDropDatabaseIfExists("hello1", out string message))
            {
                droppedNotExisting = true;
            }

            using (var cn = LocalDb.GetConnection("hello")) { }

            if (LocalDb.TryDropDatabaseIfExists("hello", out message))
            {
                droppedExisting = true;
            }

            Assert.IsTrue(droppedNotExisting && droppedExisting);
        }

        [TestMethod]
        public void TryLocalConnectionCreateSampleTable()
        {
            LocalDb.TryDropDatabase("hello", out _);

            using (var cn = LocalDb.GetConnection("hello", new InitializeStatement[]
            {                
                new InitializeStatement("dbo.Table1", 
                "DROP TABLE %obj%",
                @"CREATE TABLE %obj% (
                    [Field1] nvarchar(50) NOT NULL,
                    [Field2] nvarchar(50) NOT NULL,
                    [Field3] datetime NULL,
                    [Id] int identity(1,1) PRIMARY KEY
                )"),
                new InitializeStatement("dbo.Table2", 
                "DROP TABLE %obj%",
                @"CREATE TABLE %obj% (                    
                    [Table1Id] int NOT NULL,
                    [Field1] nvarchar(50) NOT NULL,
                    [Id] int identity(1,1) PRIMARY KEY,
                    CONSTRAINT [FK_Table2_Table1] FOREIGN KEY ([Table1Id]) REFERENCES [dbo].[Table1] ([Id])
                )")
            }))
            {
                Assert.IsTrue(LocalDb.ObjectExists(cn, "dbo.Table1"));
                Assert.IsTrue(LocalDb.ObjectExists(cn, "dbo.Table2"));
                cn.DropAllTablesAsync().Wait();                
            }

            LocalDb.TryDropDatabase("hello", out _);
        }
    }
}
