using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlServer.LocalDb;
using SqlServer.LocalDb.Models;
using System.Reflection;

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
                )")
            }))
            {
                Assert.IsTrue(LocalDb.ObjectExists(cn, "dbo.Table1"));
            }

            LocalDb.TryDropDatabase("hello", out _);
        }
    }
}
