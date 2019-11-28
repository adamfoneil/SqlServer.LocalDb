using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlServer.LocalDb;
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
        public void TryLocalConnectionCreateSampleTable()
        {
            LocalDb.TryDropDatabase("hello", out _);

            using (var cn = LocalDb.GetConnection("hello", new IfNotExistsStatement[]
            {
                new IfNotExistsStatement("dbo.Table1", 
                @"CREATE TABLE [dbo].[Table1] (
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
